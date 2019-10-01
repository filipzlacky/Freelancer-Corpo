using System;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.Entities {
    class Corporation : IUser {
        [Key]
        public int Id { get; set; }        

        [Required]    
        public IGeneralInfo Info { get; set; }
    }
}
