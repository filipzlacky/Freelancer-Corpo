using System;
using FreelancerCorp.DataAccessLayer.Enums;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Freelancer : User {

        public Sex Sex { get; set; }
        public DateTime DoB { get; set; }
        public string Location { get; set; }             

        public Freelancer() : base ("", nameof(FreelancerCorpDbContext.Freelancers), "", "")
        {

        }
        public Freelancer(Sex sex, DateTime dob, string location, string email, string name, string info) 
            : base(info, nameof(FreelancerCorpDbContext.Freelancers), name, email) {
            Sex = sex;
            DoB = dob;
            Location = location;
        }

    }
}
