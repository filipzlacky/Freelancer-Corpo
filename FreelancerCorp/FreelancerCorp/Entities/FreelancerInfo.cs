using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.Enums;
using System.ComponentModel.DataAnnotations;

namespace FreelancerCorp.Entities {
    public class FreelancerInfo : GeneralInfo { 
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Sex Sex { get; set; }

        public string ImagePath { get; set; }

        public FreelancerInfo(string name, string city, string email, DateTime dob, Sex sex) : base (name, city, email, nameof(FreelancerCorpDbContext.FreeInfos)) {
            DateOfBirth = dob;
            Sex = sex;
        }

        public FreelancerInfo(string name, string city, string email, string phone, DateTime dob, Sex sex) : base(name, city, email, phone, nameof(FreelancerCorpDbContext.FreeInfos)) {
            DateOfBirth = dob;
            Sex = sex;
        }

        public FreelancerInfo(string name, string city, string email, DateTime dob, Sex sex, string imagePath) : this(name, city, email, dob, sex) {
            ImagePath = imagePath;
        }

        public FreelancerInfo(string name, string city, string email, string phone, DateTime dob, Sex sex, string imagePath) : this(name, city, email, phone, dob, sex) {
            ImagePath = imagePath;
        }
    }
}
