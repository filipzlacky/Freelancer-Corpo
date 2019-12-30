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
using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.Services.Ratings
{
    public class RatingService : CrudQueryServiceBase<Rating, RatingDTO, RatingFilterDTO>, IRatingService
    {
        public RatingService(IMapper mapper, IRepository<Rating> categoryRepository, QueryObjectBase<RatingDTO, Rating, RatingFilterDTO, IQuery<Rating>> categoryListQuery)
            : base(mapper, categoryRepository, categoryListQuery) { }        

        public async Task<QueryResultDTO<RatingDTO, RatingFilterDTO>> ListRatingsAsync(RatingFilterDTO filter)
        {
            return await Query.ExecuteQuery(filter);
        }        

        protected override async Task<Rating> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}
