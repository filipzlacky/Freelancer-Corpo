using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Models.Accounts;
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

        public AccountsController(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        public ActionResult RegisterFreelancer()
        {
            return View("RegisterFreelancerView");
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

        public async Task<ActionResult> Profile(FreelancerDTO freelancer)
        {
            var user = await userFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);

            return View("Users/FreelancerDetailView", ToProfileModel(freelancer, user));
        }

        public async Task<ActionResult> Profile()
        {
            var user = await userFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (user.UserRole == "Freelancer")
            {
                var freelancer = await userFacade.GetFreelancerAsync(user.Id);

                return View("Users/FreelancerDetailView", ToProfileModel(freelancer, user));
            } else if (user.UserRole == "Corporation")
            {
                var corporation = await userFacade.GetCorporationAsync(user.Id);

                return View("Users/CorporationDetailView.cshtml", corporation);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> EditProfile()
        {
            var user = await userFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
            if (user.UserRole == "Freelancer")
            {
                var freelancer = await userFacade.GetFreelancerAsync(user.Id);

                return View("Users/FreelancerEditView", ToProfileModel(freelancer, user));
            }
            else if (user.UserRole == "Corporation")
            {
                var corporation = await userFacade.GetCorporationAsync(user.Id);

                return View("Users/CorporationEditView.cshtml", corporation);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> EditProfile(int id, FormCollection collection)
        {
            try
            {
                FreelancerDTO newFreelancer = new FreelancerDTO();
                newFreelancer.Id = id;

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
                    // Throw ERROR
                    throw new NotImplementedException();
                return RedirectToAction("Profile", newFreelancer);
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private FreelancerProfileModel ToProfileModel(FreelancerDTO freelancer, UserDTO user)
        {
            return new FreelancerProfileModel
            {
                FreelancerDTO = freelancer,
                UserDTO = user
            };
        }
    }
}
