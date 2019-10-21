﻿using FreelancerCorp.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Rating : IEntity {
        [Key]
        public int Id { get; set; }

        public string TableName { get; }

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

        public Rating(int score, int creatorId, string comment) {
            Score = score;
            CreatorId = creatorId;
            Comment = comment;
            TableName = nameof(FreelancerCorpDbContext.Ratings);
        }        
    }
}
