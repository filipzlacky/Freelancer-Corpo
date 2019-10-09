using FreelancerCorp.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class User : IEntity {
        [Key]
        public int Id { get; set; }

        public string TableName { get; }

        [ForeignKey(nameof(Info))]
        public int InfoId { get; set; }
        public virtual GeneralInfo Info { get; set; }

        public User (int infoId, string tableName) {
            InfoId = infoId;
            TableName = tableName;
        }
    }
}
