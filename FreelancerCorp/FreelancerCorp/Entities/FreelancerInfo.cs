using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.Enums;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.Entities {
    class FreelancerInfo : IGeneralInfo { 
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Sex Sex { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
