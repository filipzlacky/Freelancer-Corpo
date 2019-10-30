using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.BusinessLayer.Services.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure;
using FreelancerCorp.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Corporations
{
    public class CorporationService : CrudQueryServiceBase<Corporation, CorporationDTO, CorporationFilterDTO>, ICorporationService
    {
        public CorporationService(IMapper mapper, IRepository<Corporation> categoryRepository, QueryObjectBase<CorporationDTO, Corporation, CorporationFilterDTO, IQuery<Corporation>> categoryListQuery)
            : base(mapper, categoryRepository, categoryListQuery) { }


        public async Task<int[]> GetCorporationIdsByAllAsync(string location, params string[] names)
        {
            var queryResult = await Query.ExecuteQuery(new CorporationFilterDTO { CorporationNames = names, SearchedLocation = location }) ;
            return queryResult.Items.Select(corporation => corporation.Id).ToArray();
        }

        public async Task<int[]> GetCorporationIdsByLocationAsync(string location)
        {
            var queryResult = await Query.ExecuteQuery(new CorporationFilterDTO { SearchedLocation = location });
            return queryResult.Items.Select(corporation => corporation.Id).ToArray();
        }

        public async Task<int[]> GetCorporationIdsByNamesAsync(params string[] names)
        {
            var queryResult = await Query.ExecuteQuery(new CorporationFilterDTO { CorporationNames = names });
            return queryResult.Items.Select(corporation => corporation.Id).ToArray();
        }

        protected override async Task<Corporation> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

    }
}
