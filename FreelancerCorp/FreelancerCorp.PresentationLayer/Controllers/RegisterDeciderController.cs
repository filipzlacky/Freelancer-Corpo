using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerCorp.PresentationLayer.Models.Accounts
{
    public class RegisterDeciderController : Controller
    {
        public ActionResult Decide()
        {
            return View("~/Views/Accounts/RegisterDeciderView.cshtml"); ;
        }

        public ActionResult FreelancerChosen()
        {
            return View("~/Views/Accounts/RegisterFreelancerView.cshtml");
        }

        public ActionResult CorporationChosen()
        {
            return View("~/Views/Accounts/RegisterCorporationView.cshtml");
        }
    }
}