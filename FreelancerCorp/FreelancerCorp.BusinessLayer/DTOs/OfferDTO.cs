using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class OfferDTO : DTOBase
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public long Price { get; set; }

        public State State { get; set; } = State.NotAssigned;

        public string AdditionalInfo { get; set; }
        
        public int CreatorId { get; set; }

        public UserRole CreatorRole { get; set; }

        public int? ApplierId { get; set; }

        public UserRole? ApplierRole { get; set; }
    }
}
