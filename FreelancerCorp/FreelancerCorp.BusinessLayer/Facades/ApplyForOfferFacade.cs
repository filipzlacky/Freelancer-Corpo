using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.BusinessLayer.Services.Corporations;
using FreelancerCorp.BusinessLayer.Services.Freelancers;
using FreelancerCorp.BusinessLayer.Services.Offers;
using FreelancerCorp.Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class ApplyForOfferFacade : FacadeBase
    {
        private readonly FreelancerService freelancerService;
        private readonly CorporationService corporationService;
        private readonly OfferService offerService;

        public ApplyForOfferFacade(IUnitOfWorkProvider uowProvider, FreelancerService freeService, CorporationService corpService, OfferService offerService)
            :base(uowProvider)
        {
            freelancerService = freeService;
            corporationService = corpService;
            this.offerService = offerService;
        }

        public async Task<int> ApplyUserForOffer(UserAppliesForOfferDTO userAppliesForOfferDTO)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var offer = await offerService.GetAsync(userAppliesForOfferDTO.Offer.Id);

                if (userAppliesForOfferDTO.ApplierRole == UserRole.Corporation)
                {
                    var corporation = await corporationService.GetAsync(userAppliesForOfferDTO.ApplierId);
                    corporation.Offers.Add(userAppliesForOfferDTO.Offer);
                }
                else
                {
                    var freelancer = await freelancerService.GetAsync(userAppliesForOfferDTO.ApplierId);
                    freelancer.Offers.Add(userAppliesForOfferDTO.Offer);
                }

                return offer.Id;
            }
        }
    }
}
