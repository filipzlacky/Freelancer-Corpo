using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.Infrastructure.UnitOfWork;
using FreelancerCorp.BusinessLayer.Services.Offers;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.DTOs.Enums;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class OfferFacade : FacadeBase
    {
        private readonly IOfferService offerService;
        public OfferFacade(IUnitOfWorkProvider unitOfWorkProvider, IOfferService offerService) : base(unitOfWorkProvider)
        {
            this.offerService = offerService;
        }

        public async Task<int> CreateOffer(OfferDTO offer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var offerId = offerService.Create(offer);
                await uow.Commit();
                return offerId;
            }
        }

        public async Task<bool> EditOffer(OfferDTO offer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await offerService.GetAsync(offer.Id, false)) == null)
                {
                    return false;
                }
                await offerService.Update(offer);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteOffer(int offerId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await offerService.GetAsync(offerId, false)) == null)
                {
                    return false;
                }
                offerService.Delete(offerId);
                await uow.Commit();
                return true;
            }
        }

        public async Task<IEnumerable<int>> GetOffers(Category category)
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await offerService.GetOfferIdsByCategoryAsync(category));
            }
        }

        public async Task<IEnumerable<int>> GetOffers(int price)
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await offerService.GetOfferIdsByPriceAsync(price));
            }
        }

        public async Task<IEnumerable<int>> GetOffers(int price, Category category)
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await offerService.GetOfferIdsByAllAsync(price, category));
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetAllCategories()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await offerService.ListAllAsync()).Items;
            }
        }

    }
}
