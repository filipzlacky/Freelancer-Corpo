using FreelancerCorp.Infrastructure;
using FreelancerCorp.DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreelancerCorp.Enums;

namespace FreelancerCorp.DataAccessLayer.Entities {
    public class Offer : IEntity {
        [Key]
        public int Id { get; set; }

        public string TableName { get; } = "Offers";

        [Required]
        public Category Category { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public State State { get; set; } = State.NotAssigned;

        [Required]
        public long Price { get; set; }
        
        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public string CreatorRole { get; set; }

        [ForeignKey(nameof(Applier))]
        public int? ApplierId { get; set; }
        public virtual User Applier { get; set; }

        public string ApplierRole { get; set; }



        public string AdditionalInfo { get; set; }

        public Offer()
        {

        }

        public Offer(Category cat, string name, string description, long price, int creatorId, string addInfo) {
            Category = cat;
            Name = name;
            Description = description;
            Price = price;
            CreatorId = creatorId;
            TableName = nameof(FreelancerCorpDbContext.Offers);
            AdditionalInfo = addInfo;
        }
    }
}
