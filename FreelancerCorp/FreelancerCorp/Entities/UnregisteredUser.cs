using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class UnregisteredUser : User {

        public UnregisteredUser(string name, string info) : base (info, nameof(FreelancerCorpDbContext.UnregisteredUsers), name) {

        }
        
    }
}
