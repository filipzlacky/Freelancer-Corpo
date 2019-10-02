using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class Corporation : User {

        public Corporation(int id) : base(id) {

        }
    }
}
