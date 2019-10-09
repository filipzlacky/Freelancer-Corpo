using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelancerCorp.Entities {
    public class Product : Offer {

        public Product(Cathegory cat, string description, long price, int creatorId) 
            : base (cat, description, price, creatorId, nameof(FreelancerCorpDbContext.Products)) {

        }
    }
}
