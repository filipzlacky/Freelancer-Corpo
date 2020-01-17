using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Unregistered
{
    public interface IUnregisteredService
    {
        Task<QueryResultDTO<UnregisteredUserDTO, UnregisteredUserFilterDTO>> ListUnregisteredsAsync(UnregisteredUserFilterDTO filter);

        Task<UnregisteredUserDTO> GetAsync(int entityId, bool withIncludes = true);

        int Create(UnregisteredUserDTO entityDto);

        Task Update(UnregisteredUserDTO entityDto);

        void Delete(int entityId);

        Task<QueryResultDTO<UnregisteredUserDTO, UnregisteredUserFilterDTO>> ListAllAsync();
    }
}
