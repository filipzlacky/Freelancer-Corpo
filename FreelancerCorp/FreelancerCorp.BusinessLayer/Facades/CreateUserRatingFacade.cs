using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.Services.Ratings;
using FreelancerCorp.BusinessLayer.Services.Corporations;
using FreelancerCorp.BusinessLayer.Services.Freelancers;
using FreelancerCorp.Infrastructure.UnitOfWork;
using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class CreateUserRatingFacade : FacadeBase
    {
        private readonly IRatingService ratingService;
        private readonly ICorporationService corporationService;
        private readonly IFreelancerService freelancerService;

        public CreateUserRatingFacade(IUnitOfWorkProvider unitOfWorkProvider, IRatingService rating, ICorporationService corpService, IFreelancerService freeService)
            : base(unitOfWorkProvider)
        {
            ratingService = rating;
            corporationService = corpService;
            freelancerService = freeService;
        }

        public async Task<int> BindUserToRating(CreateRatingDTO createRatingDTO)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {                
                if (createRatingDTO.RatedUserRole == UserRole.Corporation)
                {
                    var corporation = await corporationService.GetAsync(createRatingDTO.RatedUserId);
                    //corporation.Ratings.Add(createRatingDTO.Rating);
                    corporation.SumRating += createRatingDTO.Rating.Score / corporation.Ratings.Count;

                    await corporationService.Update(corporation);
                }
                else
                {
                    var freelancer = await freelancerService.GetAsync(createRatingDTO.RatedUserId);
                    //freelancer.Ratings.Add(createRatingDTO.Rating);
                    freelancer.SumRating += createRatingDTO.Rating.Score / freelancer.Ratings.Count;

                    await freelancerService.Update(freelancer);
                }
                return createRatingDTO.Rating.Id;
            }
        }
    }
}
