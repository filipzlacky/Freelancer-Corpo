using System;
using FreelancerCorp.DataAccessLayer.Enums;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Freelancer : User {

        public Sex Sex { get; set; }
        public DateTime DoB { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        //public string? PhoneNumber { get; set; }        

        public Freelancer(Sex sex, DateTime dob, string location, string email, string name, string info) 
            : base(info, nameof(FreelancerCorpDbContext.Freelancers), name) {
            Sex = sex;
            DoB = dob;
            Location = location;
            Email = email;
        }

    }
}
