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

namespace FreelancerCorp.BusinessLayer.Services.Unregistered
{
    public class UnregisteredService : CrudQueryServiceBase<UnregisteredUser, UnregisteredUserDTO, UnregisteredUserFilterDTO>, IUnregisteredService
    {
        public UnregisteredService(IMapper mapper, IRepository<UnregisteredUser> categoryRepository, QueryObjectBase<UnregisteredUserDTO, UnregisteredUser, UnregisteredUserFilterDTO, IQuery<UnregisteredUser>> categoryListQuery)
            : base(mapper, categoryRepository, categoryListQuery) { }


        protected async override Task<UnregisteredUser> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDTO<UnregisteredUserDTO, UnregisteredUserFilterDTO>> ListUnregisteredsAsync(UnregisteredUserFilterDTO filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
