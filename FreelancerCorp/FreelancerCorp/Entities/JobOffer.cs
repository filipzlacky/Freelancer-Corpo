using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class JobOffer : Offer {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public JobOffer(Cathegory cat, string description, long price, int creatorId, DateTime startDate, DateTime endDate) 
            : base (cat, description, price, creatorId, nameof(FreelancerCorpDbContext.Offers)) {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
