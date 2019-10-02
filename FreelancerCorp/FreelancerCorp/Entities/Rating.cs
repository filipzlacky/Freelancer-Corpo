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
        public int RatingId { get; set; }
        [Required]
        public int Score { get; set; }

        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        [ForeignKey(nameof(RatedUser))]
        public int RatedUserId { get; set; }
        public virtual User RatedUser { get; set; }

        [Required]
        public string Comment { get; set; }

        public Rating (int score, int creatorId, string comment) {
            Score = score;
            CreatorId = creatorId;
            Comment = comment;
        }
    }
}
