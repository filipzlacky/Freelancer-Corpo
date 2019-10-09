using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class UnregisteredUser : User {

        public UnregisteredUser(int infoId) : base (infoId, nameof(FreelancerCorpDbContext.UnregisteredUsers)) {

        }
        
    }
}
