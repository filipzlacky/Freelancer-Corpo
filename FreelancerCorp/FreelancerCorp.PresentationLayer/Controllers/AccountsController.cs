using FreelancerCorp.BusinessLayer.DTOs;
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

        public ActionResult Register()
        {
            return View();
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
        public async Task<ActionResult> Logout()
        {
            var user = await userFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);           

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
