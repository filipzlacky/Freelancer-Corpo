using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.Infrastructure.UnitOfWork;
using FreelancerCorp.BusinessLayer.Services.Ratings;
using FreelancerCorp.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class RatingFacade : FacadeBase
    {
        private readonly IRatingService ratingService;
        public RatingFacade(IUnitOfWorkProvider unitOfWorkProvider, IRatingService rating) 
            : base (unitOfWorkProvider)
        {
            ratingService = rating;
        }

        public async Task<int> CreateRatingAsync(CreateRatingDTO rating)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                var ratingId = ratingService.Create(rating.Rating, rating.RatedUserId);
                await uow.Commit();
                return ratingId;
            }
        }

        public async Task<bool> EditRatingAsync(CreateRatingDTO rating)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await ratingService.GetAsync(rating.Rating.Id, false)) == null)
                {
                    return false;
                }
                await ratingService.Update(rating.Rating, rating.RatedUserId);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteRatingAsync(int ratingId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await ratingService.GetAsync(ratingId, false)) == null)
                {
                    return false;
                }
                ratingService.Delete(ratingId);
                await uow.Commit();
                return true;
            }
        }

        public async Task<RatingDTO> GetRatingsAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await ratingService.GetAsync(id));
            }
        }

        //public async Task<IEnumerable<int>> GetRatingsAsync(int[] ids)
        //{
        //    using (UnitOfWorkProvider.Create())
        //    {
        //        return (await ratingService.GetRatingsIdsByUserIdsAsync(ids));
        //    }
        //}

        //public async Task<IEnumerable<int>> GetRatingsAsync(int score, int[] ids)
        //{
        //    using (UnitOfWorkProvider.Create())
        //    {
        //        return (await ratingService.GetRatingsIdsByUserIdsScoreAsync(score, ids));
        //    }
        //}

        public async Task<QueryResultDTO<RatingDTO, RatingFilterDTO>> ListRatingsAsync(RatingFilterDTO filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await ratingService.ListRatingsAsync(filter);
            }
        }

        public async Task<IEnumerable<RatingDTO>> GetRatingsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await ratingService.ListAllAsync()).Items;
            }
        }
    }
}
