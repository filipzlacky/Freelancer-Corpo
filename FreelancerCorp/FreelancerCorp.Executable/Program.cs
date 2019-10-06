using System;
using FreelancerCorp.Entities;


namespace FreelancerCorp.Executable {
    class Program {
        static void Main(string[] args) {

            using (var db = new FreelancerCorpDbContext()) {
                db.FreeInfos.Add(new FreelancerInfo("Imre Gejza", "Brno", "noob@gmail.com", new DateTime(2002, 2, 2), Enums.Sex.FEMALE));
                db.CorpInfos.Add(new CorporationInfo("Punch Job", "Brno", "letmepunchyou@gmail.com"));

                db.SaveChanges();

                db.Freelancers.Add(new Freelancer(1));
                db.Corporations.Add(new Corporation(1));

                db.SaveChanges();
            }
        }
    }
}
