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
                
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        private async Task<(int, string)> GetUserIdRole()
        {
            var user = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            return (user.Id, user.UserRole);
        }

        public async Task<ActionResult> Enroll(int id, bool authenticated)
        {
            var offer = await OfferFacade.GetOfferAsync(id);
            string name = "";

            if (offer.CreatorRole == UserRole.Corporation)
            {
                var corporation = await UserFacade.GetCorporationAsync(offer.CreatorId);
                name = corporation.Name;
            }
            if (offer.CreatorRole == UserRole.Freelancer)
            {
                var freelancer = await UserFacade.GetFreelancerAsync(offer.CreatorId);
                name = freelancer.Name;
            }

            if (!authenticated)
            {
                var model = new OfferEnrollUnregisteredViewModel { Offer = offer, CreatorId = offer.CreatorId, Creator = name, OfferId = offer.Id };
                return View("OfferEnrollUnregisteredView", model);
            }

            UserDTO loggedInUser = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);

            var offerEnrollModel = new OfferEnrollRegisteredViewModel { Offer = offer, CreatorId = offer.CreatorId, Creator = name, OfferId = offer.Id, User = loggedInUser };
            return View("OfferEnrollRegisteredView", offerEnrollModel);

        }

        [HttpPost]
        public async Task<ActionResult> EnrollUnregistered(FormCollection collection, int id)
        {
            try
            {
                UnregisteredUserDTO newUnregistered = new UnregisteredUserDTO();

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Name":
                            newUnregistered.Name = collection[key];
                            break;
                        case "Email":
                            newUnregistered.Email = collection[key];
                            break;
                        case "PhoneNumber":
                            newUnregistered.PhoneNumber = collection[key];
                            break;
                        case "Info":
                            newUnregistered.Info = collection[key];
                            break;
                        case "Location":
                            newUnregistered.Location = collection[key];
                            break;
                    }
                }

                await UserFacade.CreateUnregisteredAsync(newUnregistered);


                // Tuto cast kodu prosim zignorujme. DB nam pri Create vzdy vracala Id 0, hoci objekt ho ma v DB ako napr. 47. 
                // Inak neviem ako ziskat toho uzivatela
                var unregisteredUsersLikeThis = await UserFacade.GetUnregisteredsAsync(new UnregisteredUserFilterDTO {
                    SearchedName = newUnregistered.Name, 
                    SearchedLocation = newUnregistered.Location });

                var offer = await OfferFacade.GetOfferAsync(id);
                offer.ApplierId = unregisteredUsersLikeThis.Items.Last().Id;
                offer.ApplierRole = UserRole.Unregistered;
                offer.State = State.InProgress;

                await OfferFacade.EditOfferAsync(offer);

                return View("EnrollmentCompleteView");
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        public async Task<ActionResult> EnrollRegistered(int id, int userId, string userRole)
        {
            var offer = await OfferFacade.GetOfferAsync(id);

            offer.ApplierId = userId;
            if (Enum.TryParse<UserRole>(userRole, out UserRole role))
            {
                offer.ApplierRole = role;
            }

            offer.State = State.InProgress;
            await OfferFacade.EditOfferAsync(offer);
            
            return View("EnrollmentCompleteView");
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
                    return View("~/Views/Home/GeneralExceptionView.cshtml");
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
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }

        }

        public async Task<ActionResult> FinishOffer(int id)
        {
            var offer = await OfferFacade.GetOfferAsync(id);
            offer.State = State.Finished;

            await OfferFacade.EditOfferAsync(offer);

            return RedirectToAction("Details", new { id = id });
        }

        // GET: OffersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            bool success = await OfferFacade.DeleteOfferAsync(id);

            if (!success)
                return View("~/Views/Home/GeneralExceptionView.cshtml");

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
                    return View("~/Views/Home/GeneralExceptionView.cshtml");

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        private async Task<string> GetUserName(int userId, bool isUnregistered)
        {
            if (isUnregistered)
            {
                var unregistered = await UserFacade.GetUnregisteredAsync(userId);
                return unregistered.Name;
            }
            var user = await UserFacade.GetUserAsync(userId);    
            return user.UserName;
        }

        private async Task<OfferDetailViewModel> InitializeOfferDetailViewModel(OfferDTO offer)
        {
            string creatorName = await GetUserName(offer.CreatorId, offer.CreatorRole == UserRole.Unregistered);
            string applierName = "";

            if (offer.ApplierId.HasValue)
            {
                applierName = await GetUserName(offer.ApplierId.Value, offer.ApplierRole == UserRole.Unregistered);
            }

            return new OfferDetailViewModel
            {
                Offer = offer,
                Creator = (creatorName, offer.CreatorId),
                Applier = (applierName, offer.ApplierId)
            };
        }

        private async Task<OfferListViewModel> InitializeOfferListViewModel(QueryResultDTO<OfferDTO, OfferFilterDTO> result)
        {
            var finalList = result.PagedResult();

            var offers = new List<(OfferDTO, string)>();
            string name;
            foreach(var offer in finalList)
            {
                name = await GetUserName(offer.CreatorId, offer.CreatorRole == UserRole.Unregistered);
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