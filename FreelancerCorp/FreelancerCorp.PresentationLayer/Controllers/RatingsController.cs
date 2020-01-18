using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
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
        public async Task<ActionResult> Details(int id, string ratedUserName)
        {
            var rating = await RatingFacade.GetRatingsAsync(id);

            var creator = await UserFacade.GetUserAsync(rating.CreatorId);            

            return View("RatingDetailView", InitializeRatingViewModel(rating, creator.UserName, ratedUserName, rating.RatedUserId, rating.RatedUserRole.ToString()));
        }

        // GET: Ratings/Create
        public async Task<ActionResult> Create(int id, string ratedUserName)
        {
            var user = await UserFacade.GetUserAsync(id);
            return View("RatingCreateView", InitializeRatingViewModel(new RatingDTO(), null, ratedUserName, id, user.UserRole));
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
                    return View("~/Views/Home/GeneralExceptionView.cshtml");
                }

                var ratedUser = await UserFacade.GetUserAsync(id);
                UserRole ratedUserRole;

                if (!Enum.TryParse<UserRole>(ratedUser.UserRole, out ratedUserRole))
                {
                    return View("~/Views/Home/GeneralExceptionView.cshtml");
                }

                int ratingId = await RatingFacade.CreateRatingAsync(CreateRating(id, creator.Id, creatorUserRole, ratedUserRole, collection));                

                return RedirectToAction("Details", ratedUserRole + "s", new { id = id });
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }        

        // GET: Ratings/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var rating = await RatingFacade.GetRatingsAsync(id);

            return View("RatingEditView", rating);
        }

        // POST: Ratings/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                var newRating = await RatingFacade.GetRatingsAsync(id);

                foreach(string key in collection.Keys)
                {
                    switch(key)
                    {
                        case "Score":
                            int newScore;
                            if (Int32.TryParse(collection[key], out newScore))
                            {
                                newRating.Score = newScore;
                            }                            
                            break;
                        case "Comment": 
                            newRating.Comment = collection[key];
                            break;
                    }
                }

                bool success = await RatingFacade.EditRatingAsync(newRating);
                if (!success)
                {
                    // THROW ERROR
                }

                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        private double? DecreaseRating(double userScore, int ratingsCount, int score)
        {
            if (ratingsCount == 1)
            {
                 return null;
            }
            else
            {
                if (userScore < score)
                {
                    return 0;
                }
                return userScore - score;
            }
        }

        private async void RemoveUserRating(RatingDTO rating)
        {
            var ratings = await RatingFacade.ListRatingsAsync(new RatingFilterDTO { SearchedRatedUsersId = new[] { rating.RatedUserId } });
            int ratingsCount = ratings.Items.Count();

            if (rating.RatedUserRole == UserRole.Corporation)
            {
                var corporation = await UserFacade.GetCorporationAsync(rating.RatedUserId);
                corporation.SumRating = DecreaseRating(corporation.SumRating.Value, ratingsCount, rating.Score);
                corporation.RatingCount -= 1;
                
                await UserFacade.EditCorporationAsync(corporation);
            }
            else
            {
                var freelancer= await UserFacade.GetFreelancerAsync(rating.RatedUserId);
                freelancer.SumRating = DecreaseRating(freelancer.SumRating.Value, ratingsCount, rating.Score);
                freelancer.RatingCount -= 1;

                await UserFacade.EditFreelancerAsync(freelancer);
            }
        }

        // GET: Ratings/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var rating = await RatingFacade.GetRatingsAsync(id);

            RemoveUserRating(rating);

            await RatingFacade.DeleteRatingAsync(id);

            string userRoleListRoute = "~/Views/" + rating.RatedUserRole + "s/" + rating.RatedUserRole + "DetailView.cshtml";

            return RedirectToAction("Details", rating.RatedUserRole + "s", new { id = rating.RatedUserId });
        }

        // POST: Ratings/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                var rating = await RatingFacade.GetRatingsAsync(id);

                return RedirectToAction("Index", "", null);
            }
            catch
            {
                return View("~/Views/Home/GeneralExceptionView.cshtml");
            }
        }

        public RatingViewModel InitializeRatingViewModel(RatingDTO rating, string creatorUserName, string ratedUserName, int ratedId, string ratedUserRole)
        {
            return new RatingViewModel
            {
                Rating = rating,
                CreatorUserName = creatorUserName,
                RatedUserName = ratedUserName,
                RatedUserId = ratedId,
                RatedUserRole = ratedUserRole
            };
        }
    }
}
