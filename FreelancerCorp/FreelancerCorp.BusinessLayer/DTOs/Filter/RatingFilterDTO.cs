using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class RatingFilterDTO :FilterDTOBase
    {
        public int[] SearchedRatedUsersId { get; set; }

        public int? SearchedScore { get; set; }
    }
}