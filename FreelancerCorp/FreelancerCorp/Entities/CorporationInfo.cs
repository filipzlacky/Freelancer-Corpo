using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.Entities {
    class CorporationInfo : IGeneralInfo {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
