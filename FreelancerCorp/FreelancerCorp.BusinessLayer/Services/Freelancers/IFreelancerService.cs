using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Freelancers
{
    public interface IFreelancerService
    {
        Task<int[]> GetFreelancerIdsByNamesAsync(params string[] names);

        Task<int[]> GetFreelancerIdsByLocationAsync(string location);

        Task<int[]> GetFreelancerIdsByAllAsync(string location, params string[] names);

        Task<FreelancerDTO> GetAsync(int entityId, bool withIncludes = true);

        int Create(FreelancerDTO entityDto);

        Task Update(FreelancerDTO entityDto);

        void Delete(int entityId);

        Task<QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>> ListAllAsync();
    }
}
