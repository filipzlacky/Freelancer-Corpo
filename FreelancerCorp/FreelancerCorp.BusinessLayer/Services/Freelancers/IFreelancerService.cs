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

        Task<QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>> ListFreelancersAsync(FreelancerFilterDTO filter);        

        Task<FreelancerDTO> GetAsync(int entityId, bool withIncludes = true);

        int Create(FreelancerDTO entityDto);

        Task Update(FreelancerDTO entityDto);

        void Delete(int entityId);

        Task<QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>> ListAllAsync();
    }
}
