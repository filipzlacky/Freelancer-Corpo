using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.Enums;

namespace FreelancerCorp.Entities {
    public interface IOffer {
        int IOfferId { get; set; }
        Cathegory Cathegory { get; set; }
        string Description { get; set; }
        long Price { get; set; }
        int UserId { get; set; }
    }
}
