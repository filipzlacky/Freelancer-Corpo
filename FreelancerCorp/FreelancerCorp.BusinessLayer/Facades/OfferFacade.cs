using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.Infrastructure.UnitOfWork;
using FreelancerCorp.BusinessLayer.Services.Offers;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using System;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class OfferFacade : FacadeBase
    {
        private readonly IOfferService offerService;
        public OfferFacade(IUnitOfWorkProvider unitOfWorkProvider, IOfferService offerService) : base(unitOfWorkProvider)
        {
            this.offerService = offerService;
        }

        public async Task<int> CreateOfferAsync(OfferDTO offer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var offerId = offerService.Create(offer);
                await uow.Commit();
                return offerId;
            }
        }

        public async Task<bool> EditOfferAsync(OfferDTO offer)
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

        public async Task<bool> AppyForOfferAsync(OfferDTO offer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await offerService.GetAsync(offer.Id, false)) == null)
                {
                    return false;
                }
                await offerService.Update(offer, (int)offer.ApplierId);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteOfferAsync(int offerId)
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

        public async Task<OfferDTO> GetOfferAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await offerService.GetAsync(id));
            }
        }

        public async Task<QueryResultDTO<OfferDTO, OfferFilterDTO>> ListOffersAsync(OfferFilterDTO filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await offerService.ListOffersAsync(filter);
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetAllOffersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await offerService.ListAllAsync()).Items;
            }
        }

    }
}
