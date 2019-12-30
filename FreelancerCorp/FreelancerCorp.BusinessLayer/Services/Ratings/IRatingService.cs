using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Ratings
{
    public interface IRatingService
    {
        //Task<int[]> GetRatingsIdsByUserIdsAsync(params int[] userIds);       

        //Task<int[]> GetRatingsIdsByUserIdsScoreAsync(int score, params int[] userIds);

        //Task<int[]> GetRatingsIdsByScoreAsync(int score);

        Task<QueryResultDTO<RatingDTO, RatingFilterDTO>> ListRatingsAsync(RatingFilterDTO filter);

        Task<RatingDTO> GetAsync(int entityId, bool withIncludes = true);

        int Create(RatingDTO entityDto);

        Task Update(RatingDTO entityDto);

        void Delete(int entityId);

        Task<QueryResultDTO<RatingDTO, RatingFilterDTO>> ListAllAsync();
    }
}
