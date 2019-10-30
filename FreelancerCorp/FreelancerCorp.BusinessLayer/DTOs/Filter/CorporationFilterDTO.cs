﻿using FreelancerCorp.BusinessLayer.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class CorporationFilterDTO : FilterDTOBase
    {
        public int[] CorporationIds { get; set; }

        public string[] CorporationNames { get; set; }

        public string SearchedLocation { get; set; }

    }
}
