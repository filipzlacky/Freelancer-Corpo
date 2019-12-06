using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Corporation : User {

        public string Address { get; set; }
       
        public Corporation() : base("", "Users", "", "", "Corporation") { }

        public Corporation(string address, string email, string name, string info) 
            : base(info, "Users", name, email, "Corporation") 
        {
            Address = address;
        }
    }
}
