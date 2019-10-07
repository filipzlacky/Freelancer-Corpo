using FreelancerCorp.Infrastructure;
using FreelancerCorp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class Offer : IEntity {
        [Key]
        public int Id { get; set; }

        //public string TableName { get; set; }

        [Required]
        public Cathegory Cathegory { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public Offer(Cathegory cat, string description, long price, int creatorId/*, string tableName*/) {
            Cathegory = cat;
            Description = description;
            Price = price;
            CreatorId = creatorId;
            //TableName = tableName;
        }
    }
}
