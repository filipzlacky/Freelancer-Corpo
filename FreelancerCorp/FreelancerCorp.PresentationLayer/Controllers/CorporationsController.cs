using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Controllers.Helpers;
using FreelancerCorp.PresentationLayer.Models.Corporations;
using FreelancerCorp.PresentationLayer.Models.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FreelancerCorp.PresentationLayer.Controllers
{
    public class CorporationsController : Controller
    {
        public const int PageSize = 6;

        private const string FilterSessionKey = "filter";

        public UserFacade UserFacade { get; set; }
        public RatingFacade RatingFacade { get; set; }
        public OfferFacade OfferFacade { get; set; }

        // GET: Corporations
        public async Task<ActionResult> Index(int page = 1)
        {            
            var allCorporations = await UserFacade.GetCorporationsAsync();            

            var model = InitializeCorporationListViewModel(new QueryResultDTO<CorporationDTO, CorporationFilterDTO> { Items = allCorporations, Filter = new CorporationFilterDTO(), RequestedPageNumber = page, PageSize = PageSize, TotalItemsCount = allCorporations.Count() });

            return View("CorporationListView", model);
        }

        private List<CorporationDTO> FilterCorporationsByRating(List<CorporationDTO> corporations, double wantedRating)
        {
            var filteredCorporations = new List<CorporationDTO>();

            foreach (var corporation in corporations)
            {
                var avgRating = RatingHelper.CountAverageRating(corporation.RatingCount, corporation.SumRating);
                if (avgRating.HasValue && avgRating >= wantedRating)
                {
                    filteredCorporations.Add(corporation);
                }
            }

            return filteredCorporations;
        }

        [HttpPost]
        public async Task<ActionResult> Index(CorporationListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;

            var result = await UserFacade.GetCorporationsAsync(model.Filter);

            if (model.Filter.SearchedAverage.HasValue)
            {
                result.Items = FilterCorporationsByRating(result.Items.ToList(), model.Filter.SearchedAverage.Value);
            }

            var newModel = InitializeCorporationListViewModel(result);
            return View("CorporationListView", newModel);
        }

        // GET: Corporations/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await UserFacade.GetCorporationAsync(id);

            var idOffers = await OfferFacade.ListOffersAsync(new OfferFilterDTO { SearchedAuthorsIds = new int[] { id } });
            model.Offers = new List<OfferDTO>(idOffers.Items);

            var appliedOffers = await OfferFacade.ListOffersAsync(new OfferFilterDTO { SearchedAppliersIds = new int[] { id } });
            model.AppliedToOffers = new List<OfferDTO>(appliedOffers.Items);

            var ratings = await RatingFacade.ListRatingsAsync(new RatingFilterDTO { SearchedRatedUsersId = new int[] { id } });
            model.Ratings = await RatingHelper.MergeRatingsCreators(UserFacade, ratings.Items.ToList());

            model.SumRating = RatingHelper.CountAverageRating(model.RatingCount, model.SumRating);

            return View("CorporationDetailView", model);
        }

        // GET: Corporations/Create
        public ActionResult Create()
        {
            return View("CorporationCreateView");
        }

        private CorporationDTO ParseCollection(FormCollection collection, CorporationDTO corporation)
        {
            foreach (string key in collection.AllKeys)
            {
                switch (key)
                {
                    case "Name":
                        corporation.Name = collection[key];
                        break;
                    case "Email":
                        corporation.Email = collection[key];
                        break;
                    case "PhoneNumber":
                        corporation.PhoneNumber = collection[key];
                        break;
                    case "Info":
                        corporation.Info = collection[key];
                        break;
                    case "Address":
                        corporation.Address = collection[key];
                        break;
                    case "Location":
                        corporation.Location = collection[key];
                        break;
                }
            }

            return corporation;
        }

        // POST: Corporations/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                CorporationDTO newCorporation = ParseCollection(collection, new CorporationDTO());                

                int newId = await UserFacade.CreateCorporationAsync(newCorporation);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        // GET: Corporations/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var corporation = await UserFacade.GetCorporationAsync(id);
            return View("CorporationEditView", corporation);
        }

        // POST: Corporations/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                CorporationDTO editedCorporation = new CorporationDTO();
                editedCorporation.Id = id;

                editedCorporation = ParseCollection(collection, editedCorporation);

                bool success = await UserFacade.EditCorporationAsync(editedCorporation);

                if (!success)
                {
                    return View("~/Views/Home/GeneralExceptionView.cshtml");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        // GET: Corporations/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            bool success = await UserFacade.DeleteCorporationAsync(id);

            if (!success)
                return View("~/Views/Home/GeneralExceptionView.cshtml");

            return RedirectToAction("Index");
        }

        // POST: Corporations/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                bool success = await UserFacade.DeleteCorporationAsync(id);

                if (!success)
                {
                    return View("~/Views/Home/GeneralExceptionView.cshtml");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        private CorporationListViewModel InitializeCorporationListViewModel(QueryResultDTO<CorporationDTO, CorporationFilterDTO> result)
        {
            var finalList = result.PagedResult();

            foreach (CorporationDTO corporation in finalList)
            {
                corporation.SumRating = RatingHelper.CountAverageRating(corporation.RatingCount, corporation.SumRating);
            }

            return new CorporationListViewModel
            {
                Corporations = new List<CorporationDTO>(finalList),
                Filter = result.Filter,
                CurrentPageIndex = result.RequestedPageNumber.HasValue ? (int)result.RequestedPageNumber : 1,
                PageCount = (int)Math.Ceiling(result.TotalItemsCount / (double)result.PageSize)
            };
        }
    }
}
