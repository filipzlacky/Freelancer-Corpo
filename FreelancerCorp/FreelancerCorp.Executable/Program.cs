using System;
using FreelancerCorp.Entities;


namespace FreelancerCorp.Executable {
    class Program {
        static void Main(string[] args) {

            using (var db = new FreelancerCorpDbContext()) {
                db.FreeInfos.Add(new FreelancerInfo("Imre Gejza", "Brno", "noob@gmail.com", new DateTime(2002, 2, 2), Enums.Sex.FEMALE));
                //db.Freelancers.Add(new Freelancer());

                db.SaveChanges();
            }
        }
    }
}
