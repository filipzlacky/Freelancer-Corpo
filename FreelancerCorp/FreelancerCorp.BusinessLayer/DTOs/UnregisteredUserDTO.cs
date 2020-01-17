using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class UnregisteredUserDTO : DTOBase
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Wrong email format, you idiot!")]
        public string Email { get; set; }

        [RegularExpression(@"[+]?[\d]+", ErrorMessage = "Wrong phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Location is required!")]
        public string Location { get; set; }

        public string Info { get; set; }

        public double? SumRating { get; set; }

        public int RatingCount { get; set; }

        public string UserName { get; set; } = "Unregistered";

        public string Password { get; set; } = "UnregisteredPassword";

    }
}
