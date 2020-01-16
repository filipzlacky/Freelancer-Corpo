using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Freelancers
{
    public class FreelancerListViewModel
    {
        public List<string> FreelancerCriteria => new List<string> { nameof(FreelancerDTO.Name), nameof(FreelancerDTO.SumRating), nameof(FreelancerDTO.Location) };

        public List<FreelancerDTO> Freelancers { get; set; }

        public FreelancerFilterDTO Filter { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }
}