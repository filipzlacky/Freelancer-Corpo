using FreelancerCorp.Infrastructure;
using FreelancerCorp.DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Offer : IEntity {
        [Key]
        public int Id { get; set; }

        public string TableName { get; }

        [Required]
        public Category Cathegory { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public string AdditionalInfo { get; set; }

        public Offer(Category cat, string description, long price, int creatorId, string tableName, string addInfo) {
            Cathegory = cat;
            Description = description;
            Price = price;
            CreatorId = creatorId;
            TableName = tableName;
            AdditionalInfo = addInfo;
        }
    }
}
