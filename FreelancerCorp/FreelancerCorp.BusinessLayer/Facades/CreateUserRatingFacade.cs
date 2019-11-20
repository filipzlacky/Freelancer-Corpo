using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var rating = await ratingService.GetAsync(createRatingDTO.Rating.Id);
                if (createRatingDTO.UserRole == UserRole.CORPORATION)
                {
                    var corporation = await corporationService.GetAsync(createRatingDTO.RatedUserId);
                    corporation.Ratings.Add(createRatingDTO.Rating);
                }
                else
                {
                    var freelancer = await freelancerService.GetAsync(createRatingDTO.RatedUserId);
                    freelancer.Ratings.Add(createRatingDTO.Rating);
                }
                return createRatingDTO.Rating.Id;
            }
        }
    }
}
