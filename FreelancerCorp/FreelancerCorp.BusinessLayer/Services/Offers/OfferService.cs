using System.Threading.Tasks;
using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.BusinessLayer.Services.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure;
using FreelancerCorp.Infrastructure.Query;

namespace FreelancerCorp.BusinessLayer.Services.Offers
{
    public class OfferService : CrudQueryServiceBase<Offer, OfferDTO, OfferFilterDTO>, IOfferService
    {
        public OfferService(IMapper mapper, IRepository<Offer> categoryRepository, QueryObjectBase<OfferDTO, Offer, OfferFilterDTO, IQuery<Offer>> categoryListQuery)
            : base(mapper, categoryRepository, categoryListQuery) { }       

        public async Task<QueryResultDTO<OfferDTO, OfferFilterDTO>> ListOffersAsync(OfferFilterDTO filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        protected async override Task<Offer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task Update(OfferDTO entityDto, int applierId)
        {
            var entity = await GetWithIncludesAsync(entityDto.Id);
            Mapper.Map(entityDto, entity);
            entity.ApplierId = applierId;
            Repository.Update(entity);
        }
    }
}
