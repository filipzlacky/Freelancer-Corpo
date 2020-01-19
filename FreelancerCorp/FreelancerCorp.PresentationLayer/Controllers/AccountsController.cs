using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Controllers.Helpers;
using FreelancerCorp.PresentationLayer.Models.Accounts;
using FreelancerCorp.PresentationLayer.Models.Freelancers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FreelancerCorp.PresentationLayer.Controllers
{
    public class AccountsController : Controller
    {
        public const int PageSize = 9;

        private const string FilterSessionKey = "filter";
        private readonly UserFacade userFacade;
        private readonly OfferFacade offerFacade;
        private readonly RatingFacade ratingFacade;

        public AccountsController(UserFacade userFacade, OfferFacade offerFacade, RatingFacade ratingFacade)
        {
            this.userFacade = userFacade;
            this.offerFacade = offerFacade;
            this.ratingFacade = ratingFacade;
        }        

        [HttpPost]
        public async Task<ActionResult> RegisterFreelancer(UserCreateFreelancerDTO userCreateDto)
        {
            try
            {
                await userFacade.RegisterFreelancer(userCreateDto);
                var authTicket = new FormsAuthenticationTicket(1, userCreateDto.UserName, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            } catch (ArgumentException)
            {
                ModelState.AddModelError("UserName", "Account with that user name already exists!");
                return View();
            }
        }

        public async Task<ActionResult> RegisterCorporation(UserCreateCorporationDTO userCreateDto)
        {
            try
            {
                await userFacade.RegisterCorporation(userCreateDto);
                var authTicket = new FormsAuthenticationTicket(1, userCreateDto.Name, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("UserName", "Account with that user name already exists!");
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            var success = await userFacade.Login(model.UserName, model.Password);

            if (success)
            {
                var authTicket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                var decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    decodedUrl = Server.UrlDecode(returnUrl);
                }

                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Wrong username or password!");
            return View();
        }

        public new async Task<ActionResult> Profile()
        {
            var user = await userFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);

            var idOffers = await offerFacade.ListOffersAsync(new OfferFilterDTO { SearchedAuthorsIds = new int[] { user.Id } });
            var idAppliedOffers = await offerFacade.ListOffersAsync(new OfferFilterDTO { SearchedAppliersIds = new int[] { user.Id } });
            var idRatings = await ratingFacade.ListRatingsAsync(new RatingFilterDTO { SearchedRatedUsersId = new int[] { user.Id } });

            if (user.UserRole == "Freelancer")
            {
                var freelancer = await userFacade.GetFreelancerAsync(user.Id);
               
                freelancer.Offers = new List<OfferDTO>(idOffers.Items);
                freelancer.AppliedToOffers = new List<OfferDTO>(idAppliedOffers.Items);
                freelancer.Ratings = await RatingHelper.MergeRatingsCreators(userFacade, idRatings.Items.ToList());

                freelancer.SumRating = RatingHelper.CountAverageRating(freelancer.RatingCount, freelancer.SumRating);

                return View("Users/FreelancerDetailView", ToProfileModel(freelancer, user.UserName));
            } 
            else if (user.UserRole == "Corporation")
            {
                var corporation = await userFacade.GetCorporationAsync(user.Id);

                corporation.Offers = new List<OfferDTO>(idOffers.Items);
                corporation.AppliedToOffers = new List<OfferDTO>(idAppliedOffers.Items);
                corporation.Ratings = await RatingHelper.MergeRatingsCreators(userFacade, idRatings.Items.ToList());

                corporation.SumRating = RatingHelper.CountAverageRating(corporation.RatingCount, corporation.SumRating);

                return View("Users/CorporationDetailView", corporation);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> EditProfile()
        {            
            var user = await userFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (user.UserRole == "Freelancer")
            {
                var freelancer = await userFacade.GetFreelancerAsync(user.Id);

                return View("Users/FreelancerEditView", freelancer);
            }
            else if (user.UserRole == "Corporation")
            {
                var corporation = await userFacade.GetCorporationAsync(user.Id);

                return View("Users/CorporationEditView", corporation);
            }

            return RedirectToAction("Index", "Home");
        }

        private async Task<ActionResult> EditFreelancerProfile(UserDTO user, FormCollection collection)
        {
            try
            {
                FreelancerDTO newFreelancer = new FreelancerDTO();
                newFreelancer.Id = user.Id;

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {                        
                        case "Name":
                            newFreelancer.Name = collection[key];
                            break;
                        case "Email":
                            newFreelancer.Email = collection[key];
                            break;
                        case "Info":
                            newFreelancer.Info = collection[key];
                            break;
                        case "PhoneNumber":
                            newFreelancer.PhoneNumber = collection[key];
                            break;
                        case "Location":
                            newFreelancer.Location = collection[key];
                            break;
                        case "Sex":
                            if (Enum.TryParse(collection[key], out Sex newSex))
                            {
                                newFreelancer.Sex = newSex;
                            }
                            break;
                        case "DoB":
                            if (DateTime.TryParse(collection[key], out DateTime newDate))
                            {
                                newFreelancer.DoB = newDate;
                            }
                            break;
                    }
                }

                bool success = await userFacade.EditFreelancerAsync(newFreelancer);
                if (!success)
                    return View("~/Views/Home/GeneralExceptionView.cshtml");

                return RedirectToAction("Profile");
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        private async Task<ActionResult> EditCorporationProfile(UserDTO user, FormCollection collection)
        {
            try
            {
                CorporationDTO newCorporation = new CorporationDTO();
                newCorporation.Id = user.Id;

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Name":
                            newCorporation.Name = user.UserName;
                            break;
                        case "Email":
                            newCorporation.Email = collection[key];
                            break;
                        case "Info":
                            newCorporation.Info = collection[key];
                            break;
                        case "PhoneNumber":
                            newCorporation.PhoneNumber = collection[key];
                            break;
                        case "Address":
                            newCorporation.Address = collection[key];
                            break;
                        case "Location":
                            newCorporation.Location = collection[key];
                            break;
                    }
                }

                bool success = await userFacade.EditCorporationAsync(newCorporation);
                if (!success)
                    return View("~/Views/Home/GeneralExceptionView.cshtml");

                return RedirectToAction("Profile");
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditProfile(FormCollection collection)
        {
            var user = await userFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (user.UserRole == "Freelancer")
            {
                return await EditFreelancerProfile(user, collection);
            }
            if (user.UserRole == "Corporation")
            {
                return await EditCorporationProfile(user, collection);
            }
            return RedirectToAction("Profile");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private FreelancerDetailViewModel ToProfileModel(FreelancerDTO freelancer, string user)
        {
            return new FreelancerDetailViewModel
            {
                Freelancer = freelancer,
                UserName = user
            };
        }
    }
}
