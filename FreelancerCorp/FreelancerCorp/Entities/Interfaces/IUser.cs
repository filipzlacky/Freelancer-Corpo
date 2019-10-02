using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.Entities {
    public interface IUser {
        int IUserId { get; set; }
        int GeneralInfoId { get; set; }
    }
}
