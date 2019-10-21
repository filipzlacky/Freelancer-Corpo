using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Corporation : User {

        public Corporation(string info) : base(info, nameof(FreelancerCorpDbContext.Corporations)) {

        }
    }
}
