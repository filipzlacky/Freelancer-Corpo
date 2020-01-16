using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreelancerCorp.Enums;


namespace FreelancerCorp.DataAccessLayer.Entities {
    public class UnregisteredUser : User {

        public UnregisteredUser() : base("", "Users", "", "", "Unregistered")
        { }

        public UnregisteredUser(string name, string info) : base (info, "Users", name, "", "Unregistered") {

        }
        
    }
}
