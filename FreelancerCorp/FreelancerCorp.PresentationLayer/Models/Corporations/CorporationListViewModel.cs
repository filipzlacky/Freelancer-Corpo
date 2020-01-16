using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Corporations
{
    public class CorporationListViewModel
    {
        public List<string> CorporationCriteria => new List<string> { nameof(CorporationDTO.Name), nameof(CorporationDTO.SumRating), nameof(CorporationDTO.Address) };

        public List<CorporationDTO> Corporations { get; set; }

        public CorporationFilterDTO Filter { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }
}