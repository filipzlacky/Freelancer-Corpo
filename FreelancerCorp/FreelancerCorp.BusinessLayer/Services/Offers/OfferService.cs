using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
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

        public async Task<int[]> GetOfferIdsByAllAsync(int price, Category category)
        {
            var queryResult = await Query.ExecuteQuery(new OfferFilterDTO { SearchedCategory = category, SearchedPrice = price });
            return queryResult.Items.Select(offer => offer.Id).ToArray();
        }

        public async Task<int[]> GetOfferIdsByCategoryAsync(Category category)
        {
            var queryResult = await Query.ExecuteQuery(new OfferFilterDTO { SearchedCategory = category });
            return queryResult.Items.Select(offer => offer.Id).ToArray();
        }

        public async Task<int[]> GetOfferIdsByPriceAsync(int price)
        {
            var queryResult = await Query.ExecuteQuery(new OfferFilterDTO { SearchedPrice = price });
            return queryResult.Items.Select(offer => offer.Id).ToArray();
        }        

        protected async override Task<Offer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public int Create(OfferDTO entityDto, int applierId)
        {
            var entity = Mapper.Map<Offer>(entityDto);
            entity.ApplierId = applierId;
            Repository.Create(entity);
            return entity.Id;
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
