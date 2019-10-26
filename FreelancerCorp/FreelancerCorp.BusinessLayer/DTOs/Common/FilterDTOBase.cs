﻿namespace FreelancerCorp.BusinessLayer.DTOs.Common
{
    public class FilterDTOBase
    {        
        public int? RequestedPageNumber { get; set; }

        public int PageSize { get; set; }
      
        public string SortCriteria { get; set; }
        
        public bool SortAscending { get; set; }
    }
}
