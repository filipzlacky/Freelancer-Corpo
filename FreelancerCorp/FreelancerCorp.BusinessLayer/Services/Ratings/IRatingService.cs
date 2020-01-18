using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Ratings
{
    public interface IRatingService
    {
        Task<QueryResultDTO<RatingDTO, RatingFilterDTO>> ListRatingsAsync(RatingFilterDTO filter);

        Task<RatingDTO> GetAsync(int entityId, bool withIncludes = true);

        int Create(RatingDTO entityDto);

        Task Update(RatingDTO entityDto);

        void Delete(int entityId);

        Task<QueryResultDTO<RatingDTO, RatingFilterDTO>> ListAllAsync();
    }
}
