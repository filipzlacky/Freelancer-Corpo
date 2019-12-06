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
        public const int PageSize = 9;

        private const string FilterSessionKey = "filter";

        public OfferFacade OfferFacade { get; set; }
        public UserFacade UserFacade { get; set; }

        // GET: OffersController
        public async Task<ActionResult> Index(int page = 1)
        {
            var allOffers = await OfferFacade.GetAllOffersAsync();            

            var model = InitializeOfferListViewModel(new QueryResultDTO<OfferDTO, OfferFilterDTO> { Items = allOffers });
            return View("OfferListView", model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(OfferListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;

            var result = await OfferFacade.ListOffersAsync(model.Filter);

            var newModel = InitializeOfferListViewModel(result);
            return View("OfferListView", newModel);
        }

        // GET: OffersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await OfferFacade.GetOfferAsync(id);
            return View("OfferDetailView", model);
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
                string title = "", details = "";

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Title":
                            title = collection[key];
                            break;
                        case "Details":
                            details = collection[key];
                            break;
                        case "Price":
                            if (!Double.TryParse(collection[key], out double value))
                            {
                                // Throw error
                            }
                            newOffer.Price = (long)Math.Truncate(value);
                            break;
                        case "Category":
                            if (!Enum.TryParse(collection[key], out Category newSex))
                            {
                                // THROW ERROR
                            }
                            newOffer.Cathegory = newSex;
                            break;
                    }
                }

                newOffer.Description = title + ": " + details;
                var user = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
                newOffer.CreatorId = user.Id;
                newOffer.CreatorRole = (UserRole)Enum.Parse(typeof(UserRole), user.UserRole);

                int newId = await OfferFacade.CreateOfferAsync(newOffer);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                return View("~/Views/Home/Index.cshtml");
            }
        }

        // GET: OffersController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = OfferFacade.GetOfferAsync(id);
            return View("OfferEditView", model);
        }

        // POST: OffersController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                OfferDTO newOffer = new OfferDTO();
                string title = "", details = "";

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Title":
                            title = collection[key];
                            break;
                        case "Details":
                            details = collection[key];
                            break;
                        case "Price":
                            if (!Double.TryParse(collection[key], out double value))
                            {
                                // Throw error
                            }
                            newOffer.Price = (long)Math.Truncate(value);
                            break;
                        case "Category":
                            if (!Enum.TryParse(collection[key], out Category newSex))
                            {
                                // THROW ERROR
                            }
                            newOffer.Cathegory = newSex;
                            break;
                    }
                }

                newOffer.Description = title + ": " + details;

                bool success = await OfferFacade.EditOfferAsync(newOffer);
                if (!success) throw new NotImplementedException();
                return RedirectToAction("Index");
            }
            catch
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

        private OfferListViewModel InitializeOfferListViewModel(QueryResultDTO<OfferDTO, OfferFilterDTO> result)
        {            
            return new OfferListViewModel
            {
                Offers = new List<OfferDTO>(result.Items),
                Filter = result.Filter
            };
        }
    }
}