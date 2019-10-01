using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.Entities {
    public interface IUser {
        int Id { get; set; }
        IGeneralInfo Info { get; set; }
    }
}
