using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Offers
{
    public interface IOfferService
    {
        Task<QueryResultDTO<OfferDTO, OfferFilterDTO>> ListOffersAsync(OfferFilterDTO filter);        

        Task<OfferDTO> GetAsync(int entityId, bool withIncludes = true);

        int Create(OfferDTO entityDto);

        Task Update(OfferDTO entityDto);
        Task Update(OfferDTO entityDto, int applierId);

        void Delete(int entityId);

        Task<QueryResultDTO<OfferDTO, OfferFilterDTO>> ListAllAsync();
    }
}
