using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Corporations
{
    public interface ICorporationService
    {
        Task<int[]> GetCorporationIdsByNamesAsync(params string[] names);

        Task<int[]> GetCorporationIdsByLocationAsync(string location);

        Task<int[]> GetCorporationIdsByAllAsync(string location, params string[] names);

        Task<CorporationDTO> GetAsync(int entityId, bool withIncludes = true);

        int Create(CorporationDTO entityDto);

        Task Update(CorporationDTO entityDto);

        void Delete(int entityId);

        Task<QueryResultDTO<CorporationDTO, CorporationFilterDTO>> ListAllAsync();
    }
}
