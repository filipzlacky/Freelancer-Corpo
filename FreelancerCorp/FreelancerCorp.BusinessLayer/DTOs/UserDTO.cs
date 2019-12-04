using FreelancerCorp.BusinessLayer.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class UserDTO : DTOBase
    {
        [Required]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string PasswordSalt { get; set; }

        [Required, StringLength(100)]
        public string PasswordHash { get; set; }
    }
}
