using FreelancerCorp.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class GeneralInfo : IEntity {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public GeneralInfo(string name, string city, string email, string tableName) {
            Name = name;
            City = city;
            Email = email;
            TableName = tableName;
        }

        public GeneralInfo(string name, string city, string email, string phone, string tableName) : this(name, city, email, tableName) {
            PhoneNumber = phone;
        }
    }
}
