using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class User {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Info))]
        public int InfoId { get; set; }
        public virtual GeneralInfo Info { get; set; }

        public User (int infoId) {
            InfoId = infoId;
        }
    }
}
