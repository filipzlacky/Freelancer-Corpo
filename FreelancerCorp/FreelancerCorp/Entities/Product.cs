using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    class Product : IOffer {
        [Key]
        public int Id { get; set; }
        [Required]
        public Cathegory Cathegory { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long Price { get; set; }
        [ForeignKey("IUserId")]
        public int CreatorId { get; set; }
    }
}
