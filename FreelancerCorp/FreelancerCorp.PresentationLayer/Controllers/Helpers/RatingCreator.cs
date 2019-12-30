using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerCorp.PresentationLayer.Controllers.Helpers
{
    public class RatingCreator
    {
        private static int CorrectedScore(int score)
        {
            if (score < 0)
            {
                return 0;
            }
            if (score > 100)
            {
                return 100;
            }
            return score;
        }

        public static RatingDTO CreateRating(int ratedId, int creatorId, UserRole creatorUserRole, UserRole ratedUserRole, FormCollection collection)
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
                            newRating.Score = CorrectedScore(value);
                        }
                        break;
                };
            }

            return newRating;
        }
    }
}