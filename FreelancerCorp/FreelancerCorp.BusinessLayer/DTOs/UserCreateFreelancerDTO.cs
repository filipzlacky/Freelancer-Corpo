using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class UserCreateFreelancerDTO
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Wrong email format, you idiot!")]
        public string Email { get; set; }

        [RegularExpression(@"[+]?[\d]+", ErrorMessage = "Wrong phone number format, you idiot!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Sex is required!")]
        public Sex Sex { get; set; }

        [Required(ErrorMessage = "Location is required!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Date of birth is required!")]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        public string Info { get; set; }

        [Required(ErrorMessage = "UserName is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 30")]
        public string Password { get; set; }
    }
}
