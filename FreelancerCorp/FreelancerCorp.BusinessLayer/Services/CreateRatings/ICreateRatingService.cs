using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.CreateRatings
{
    public interface ICreateRatingService
    {
        int CreateNewRating(CreateRatingDTO createRatingDTO);

    }
}
