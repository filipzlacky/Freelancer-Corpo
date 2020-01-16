using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FreelancerCorp.PresentationLayer.Controllers
{
    public class OffersController : Controller
    {
        public const int PageSize = 8;

        private const string FilterSessionKey = "filter";

        public OfferFacade OfferFacade { get; set; }
        public UserFacade UserFacade { get; set; }

        // GET: OffersController
        public async Task<ActionResult> Index(int page = 1)
        {
            var allOffers = await OfferFacade.GetAllOffersAsync();            

            var model = await InitializeOfferListViewModel(new QueryResultDTO<OfferDTO, OfferFilterDTO> { Items = allOffers, Filter = new OfferFilterDTO(), RequestedPageNumber = page, PageSize = PageSize, TotalItemsCount = allOffers.Count() });
            return View("OfferListView", model);
        }

        //private List<OfferDTO>

        [HttpPost]
        public async Task<ActionResult> Index(OfferListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;

            var result = await OfferFacade.ListOffersAsync(model.Filter);

            var newModel = await InitializeOfferListViewModel(result);
            return View("OfferListView", newModel);
        }

        // GET: OffersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await OfferFacade.GetOfferAsync(id);
            var newModel = await InitializeOfferDetailViewModel(model);
            return View("OfferDetailView", newModel);
        }

        // GET: OffersController/Create
        public ActionResult Create()
        {
            return View("OfferCreateView");
        }

        // POST: OffersController/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                OfferDTO newOffer = new OfferDTO();
                string name = "", details = "";

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Name":
                            name = collection[key];
                            newOffer.Name = collection[key];
                            break;
                        case "AdditionalInfo":
                            newOffer.AdditionalInfo = collection[key];
                            break;
                        case "Details":
                            details = collection[key];
                            break;
                        case "Price":
                            if (Double.TryParse(collection[key], out double value))
                            {
                                newOffer.Price = (long)Math.Truncate(value);
                            }
                            
                            break;
                        case "Category":
                            if (Enum.TryParse(collection[key], out Category newSex))
                            {
                                newOffer.Category = newSex;
                            }                            
                            break;
                    }
                }

                newOffer.Description = name + ": " + details;
                var user = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
                newOffer.CreatorId = user.Id;
                newOffer.CreatorRole = (UserRole)Enum.Parse(typeof(UserRole), user.UserRole);

                int newId = await OfferFacade.CreateOfferAsync(newOffer);            

                return RedirectToAction("Index");
            }
            catch
            {
                
                return View("~/Views/Home/Index.cshtml");
            }
        }

        private async Task<(int, string)> GetUserIdRole()
        {
            var user = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            return (user.Id, user.UserRole);
        }

        public async Task<ActionResult> Enrol(int id)
        {
            throw new NotImplementedException("Caka sa na dohodnutie sa");
        }

        // GET: OffersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await OfferFacade.GetOfferAsync(id);
            return View("OfferEditView", model);
        }      

        // POST: OffersController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                UserRole userRole;
                OfferDTO newOffer = new OfferDTO();
                newOffer.Id = id;
                var idRoleTuple = await GetUserIdRole();

                if (!Enum.TryParse<UserRole>(idRoleTuple.Item2, out userRole))
                {
                    return View("~/Views/Home/Index.cshtml");
                }

                newOffer.CreatorId = idRoleTuple.Item1;
                newOffer.CreatorRole = userRole;

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Name":
                            newOffer.Name = collection[key];
                            break;
                        case "Description":
                            newOffer.Description = collection[key];
                            break;
                        case "AdditionalInfo":
                            newOffer.AdditionalInfo = collection[key];
                            break;
                        case "Price":
                            if (Double.TryParse(collection[key], out double value))
                            {
                                newOffer.Price = (long)Math.Truncate(value);
                            }                            
                            break;
                        case "Category":
                            if (Enum.TryParse(collection[key], out Category newSex))
                            {
                                newOffer.Category = newSex;
                            }                            
                            break;
                    }
                }                

                bool success = await OfferFacade.EditOfferAsync(newOffer);
                if (!success) throw new NotImplementedException();
                return RedirectToAction("Details", new { id = newOffer.Id });
            }
            catch (Exception ex)
            {
                return View("~/Views/Home/Index.cshtml");
            }

        }

        // GET: OffersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            bool success = await OfferFacade.DeleteOfferAsync(id);

            if (!success)
                // THROW ERROR
                throw new NotImplementedException();

            return RedirectToAction("Index");
        }

        // POST: OffersController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                bool success = await OfferFacade.DeleteOfferAsync(id);

                if (!success)
                    // THROW ERROR
                    throw new NotImplementedException();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        private async Task<string> GetCreatorName(OfferDTO offer)
        {
            var creator = await UserFacade.GetUserAsync(offer.CreatorId);            
            return creator.UserName;
        }

        private async Task<OfferDetailViewModel> InitializeOfferDetailViewModel(OfferDTO offer)
        {
            string creatorName = await GetCreatorName(offer), applierName;

            return new OfferDetailViewModel
            {
                Offer = offer,
                Creator = (creatorName, offer.CreatorId)
            };
        }

        private async Task<OfferListViewModel> InitializeOfferListViewModel(QueryResultDTO<OfferDTO, OfferFilterDTO> result)
        {
            var finalList = result.PagedResult();

            var offers = new List<(OfferDTO, string)>();
            string name;
            foreach(var offer in finalList)
            {
                name = await GetCreatorName(offer);
                offers.Add((offer,name));
            }
            return new OfferListViewModel
            {
                Offers = offers,
                Filter = result.Filter,
                CurrentPageIndex = result.RequestedPageNumber.HasValue ? (int)result.RequestedPageNumber : 1,
                PageCount = (int)Math.Ceiling(result.TotalItemsCount / (double)result.PageSize)
            };
        }

        private OfferEditViewModel InitializeOfferEditViewModel(OfferDTO offer)
        {
            return new OfferEditViewModel
            {
                Name = offer.Name,
                Description = offer.Description,
                Price = offer.Price,
                Category = offer.Category,
                AdditionalInfo = offer.AdditionalInfo
            };
        }
    }
}