using System;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.Entities {
    public class GeneralInfo {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public GeneralInfo(string name, string city, string email) {
            Name = name;
            City = city;
            Email = email;
        }

        public GeneralInfo(string name, string city, string email, string phone) : this(name, city, email) {
            PhoneNumber = phone;
        }
    }
}
