using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.Facades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelancerCorp.PresentationLayer.Controllers.Helpers
{
    public class RatingHelper
    {       
        public static double? CountAverageRating(int ratingsCount, double? sumRating)
        {
            if (sumRating.HasValue)
            {
                return sumRating.Value / ratingsCount;
            }

            return null;
        }

        public static async Task<List<(RatingDTO rating, string creator)>> MergeRatingsCreators(UserFacade userFacade, List<RatingDTO> ratings)
        {
            var result = new List<(RatingDTO rating, string creator)>();

            foreach(var rating in ratings) {
                var creator = await userFacade.GetUserAsync(rating.CreatorId);
                result.Add((rating, creator.UserName));
            }

            return result;
        }
    }
}