using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.Entities {
    public class CorporationInfo : GeneralInfo {

        public CorporationInfo(string name, string city, string email) : base(name, city, email/*, "CorporationInfo"*/) {
        }

        public CorporationInfo(string name, string city, string email, string phone) : base(name, city, email, phone/*, "CorporationInfo"*/) {
        }
    }
}
