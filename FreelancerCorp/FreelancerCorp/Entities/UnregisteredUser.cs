using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.Entities {
    class UnregisteredUser : IUser {
        [Key]
        public int Id { get; set; }

        [Required]
        public IGeneralInfo Info { get; set; }
    }
}
