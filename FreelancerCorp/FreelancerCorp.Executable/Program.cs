using System;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.DataAccessLayer.Enums;
using FreelancerCorp.DataAccessLayer;


namespace FreelancerCorp.Executable {
    class Program {
        static void Main(string[] args) {

            using (var db = new FreelancerCorpDbContext()) {
                db.Freelancers.Add(new Freelancer("Imre Gejza"));
                db.Corporations.Add(new Corporation("Punch Job"));
                db.Freelancers.Add(new Freelancer("Jozko Vajda"));

                db.SaveChanges();
            }
        }
    }
}
