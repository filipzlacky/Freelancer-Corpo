using FreelancerCorp.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class User : IEntity {
        [Key]
        public int Id { get; set; }

        public string TableName { get; }

        public string Name { get; set; }

        public string Info { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }  

        public User (string info, string tableName, string name, string email) {
            TableName = tableName;
            Info = info;
            Name = name;
            Email = email;
        }
    }
}
