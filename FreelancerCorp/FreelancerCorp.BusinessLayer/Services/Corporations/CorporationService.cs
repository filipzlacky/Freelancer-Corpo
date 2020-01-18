using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
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

        
        public async Task<QueryResultDTO<CorporationDTO, CorporationFilterDTO>> ListCorporationsAsync(CorporationFilterDTO filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        protected override async Task<Corporation> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

    }
}
