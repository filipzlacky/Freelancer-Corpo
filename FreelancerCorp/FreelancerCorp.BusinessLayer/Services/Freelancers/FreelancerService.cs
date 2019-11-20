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

namespace FreelancerCorp.BusinessLayer.Services.Freelancers
{
    public class FreelancerService : CrudQueryServiceBase<Freelancer, FreelancerDTO, FreelancerFilterDTO>, IFreelancerService
    {
        public FreelancerService(IMapper mapper, IRepository<Freelancer> categoryRepository, QueryObjectBase<FreelancerDTO, Freelancer, FreelancerFilterDTO, IQuery<Freelancer>> categoryListQuery)
            : base(mapper, categoryRepository, categoryListQuery) { }

        //public async Task<int[]> GetFreelancerIdsByAllAsync(string location, params string[] names)
        //{
        //    var queryResult = await Query.ExecuteQuery(new FreelancerFilterDTO { SearchedLocation = location, SearchedFreelancerNames = names });
        //    return queryResult.Items.Select(freelancer => freelancer.Id).ToArray();
        //}

        //public async Task<int[]> GetFreelancerIdsByLocationAsync(string location)
        //{
        //    var queryResult = await Query.ExecuteQuery(new FreelancerFilterDTO { SearchedLocation = location });
        //    return queryResult.Items.Select(freelancer => freelancer.Id).ToArray();
        //}

        //public async Task<int[]> GetFreelancerIdsByNamesAsync(params string[] names)
        //{
        //    var queryResult = await Query.ExecuteQuery(new FreelancerFilterDTO { SearchedFreelancerNames = names });
        //    return queryResult.Items.Select(freelancer => freelancer.Id).ToArray();
        //}        

        protected async override Task<Freelancer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>> ListFreelancersAsync(FreelancerFilterDTO filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
