using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Freelancer : User {

        public Freelancer() : base("", nameof(FreelancerCorpDbContext.Freelancers)) { }

        public Freelancer(string info) : base(info, nameof(FreelancerCorpDbContext.Freelancers)) {

        }

    }
}
