using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class Rating {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Score { get; set; }
        [ForeignKey("IUserId")]
        public int UserId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
