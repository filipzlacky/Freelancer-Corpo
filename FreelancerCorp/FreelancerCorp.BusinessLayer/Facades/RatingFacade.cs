using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.Infrastructure.UnitOfWork;
using FreelancerCorp.BusinessLayer.Services.Ratings;
using FreelancerCorp.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Services.Corporations;
using FreelancerCorp.BusinessLayer.Services.Freelancers;
using FreelancerCorp.BusinessLayer.DTOs.Enums;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class RatingFacade : FacadeBase
    {
        private readonly IRatingService ratingService;
        private readonly ICorporationService corporationService;
        private readonly IFreelancerService freelancerService;

        public RatingFacade(IUnitOfWorkProvider unitOfWorkProvider, IRatingService rating, ICorporationService corporation, IFreelancerService freelancer) 
            : base (unitOfWorkProvider)
        {
            ratingService = rating;
            corporationService = corporation;
            freelancerService = freelancer;
        }

        public async Task<int> CreateRatingAsync(RatingDTO rating)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {                                
                if (rating.RatedUserRole == UserRole.Corporation)
                {
                    var corporation = await corporationService.GetAsync(rating.RatedUserId);
                    
                    if (!corporation.SumRating.HasValue)
                    {
                        corporation.SumRating = 0;
                    }
                    corporation.SumRating += rating.Score;
                    corporation.RatingCount += 1;
                    await corporationService.Update(corporation);
                }
                else
                {
                    var freelancer = await freelancerService.GetAsync(rating.RatedUserId);

                    if (!freelancer.SumRating.HasValue)
                    {
                        freelancer.SumRating = 0;
                    }
                    freelancer.SumRating += rating.Score;
                    freelancer.RatingCount += 1;

                    await freelancerService.Update(freelancer);
                }
                var ratingId = ratingService.Create(rating);
                await uow.Commit();
                return ratingId;
            }
        }

        public async Task<bool> EditRatingAsync(RatingDTO rating)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await ratingService.GetAsync(rating.Id, false)) == null)
                {
                    return false;
                }
                await ratingService.Update(rating);
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
