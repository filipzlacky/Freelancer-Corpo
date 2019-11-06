using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Services.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using AutoMapper;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure;
using System.Linq;

namespace FreelancerCorp.BusinessLayer.Services.Ratings
{
    public class RatingService : CrudQueryServiceBase<Rating, RatingDTO, RatingFilterDTO>, IRatingService
    {
        public RatingService(IMapper mapper, IRepository<Rating> categoryRepository, QueryObjectBase<RatingDTO, Rating, RatingFilterDTO, IQuery<Rating>> categoryListQuery)
            : base(mapper, categoryRepository, categoryListQuery) { }

        public async Task<int[]> GetRatingsIdsByScoreAsync(int score)
        {
            var queryResult = await Query.ExecuteQuery(new RatingFilterDTO { SearchedScore = score });
            return queryResult.Items.Select(rating => rating.Id).ToArray();
        }

        public async Task<int[]> GetRatingsIdsByUserIdsAsync(params int[] userIds)
        {
            var queryResult = await Query.ExecuteQuery(new RatingFilterDTO { SearchedRatedUsers = userIds });
            return queryResult.Items.Select(rating => rating.Id).ToArray();
        }

        public async Task<int[]> GetRatingsIdsByUserIdsScoreAsync(int score, params int[] userIds)
        {
            var queryResult = await Query.ExecuteQuery(new RatingFilterDTO { SearchedRatedUsers = userIds, SearchedScore = score});
            return queryResult.Items.Select(rating => rating.Id).ToArray();
        }

        protected override async Task<Rating> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}
