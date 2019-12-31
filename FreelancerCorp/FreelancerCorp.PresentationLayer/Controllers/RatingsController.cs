using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Models.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FreelancerCorp.PresentationLayer.Controllers
{
    public class RatingsController : Controller
    {
        public RatingFacade RatingFacade { get; set; }
        public UserFacade UserFacade { get; set; }

        // GET: Ratings/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ratings/Create
        public ActionResult Create(int id, string ratedUserName)
        {
            return View("CreateRAting", new RatingCreateViewModel { RatedUserName = ratedUserName, Rating = new RatingDTO() });
        }

        private RatingDTO CreateRating(int ratedId, int creatorId, UserRole creatorUserRole, UserRole ratedUserRole, FormCollection collection)
        {
            var newRating = new RatingDTO();
            newRating.RatedUserId = ratedId;
            newRating.RatedUserRole = ratedUserRole;

            newRating.CreatorId = creatorId;
            newRating.CreatorRole = creatorUserRole;

            foreach (string key in collection.AllKeys)
            {
                switch (key)
                {
                    case "Rating.Comment":
                        newRating.Comment = collection[key];
                        break;
                    case "Rating.Score":
                        if (Int32.TryParse(collection[key], out int value))
                        {
                            newRating.Score = value;
                        }
                        break;
                };
            }

            return newRating;
        }

        // POST: Ratings/Create
        [HttpPost]
        public async Task<ActionResult> Create(int id, FormCollection collection)
        {
            try
            {
                var creator = await UserFacade.GetUserAccordingToUsernameAsync(User.Identity.Name);
                UserRole creatorUserRole;                

                if (!Enum.TryParse<UserRole>(creator.UserRole, out creatorUserRole))
                {
                    return View("~/Views/Home/Index.cshtml");
                }

                var ratedUser = await UserFacade.GetUserAsync(id);
                UserRole ratedUserRole;

                if (!Enum.TryParse<UserRole>(ratedUser.UserRole, out ratedUserRole))
                {
                    return View("~/Views/Home/Index.cshtml");
                }

                int ratingId = await RatingFacade.CreateRatingAsync(CreateRating(id, creator.Id, creatorUserRole, ratedUserRole, collection));                

                return RedirectToAction("Details", creatorUserRole.ToString() + "s", new { id = id });
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }        

        // GET: Ratings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ratings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ratings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
