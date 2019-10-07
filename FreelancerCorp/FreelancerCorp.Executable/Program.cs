using System;
using FreelancerCorp.Entities;


namespace FreelancerCorp.Executable {
    class Program {
        static void Main(string[] args) {

            using (var db = new FreelancerCorpDbContext()) {
                db.FreeInfos.Add(new FreelancerInfo("Imre Gejza", "Brno", "noob@gmail.com", new DateTime(2002, 2, 2), Enums.Sex.FEMALE));
                db.CorpInfos.Add(new CorporationInfo("Punch Job", "Brno", "letmepunchyou@gmail.com"));
                db.FreeInfos.Add(new FreelancerInfo("Jozko Vajda", "Zilina", "jozko.vajda@zilina.sk", new DateTime(1955, 7, 31), Enums.Sex.MALE));

                db.SaveChanges();

                db.Freelancers.Add(new Freelancer(1));
                db.Freelancers.Add(new Freelancer(2));
                db.Corporations.Add(new Corporation(1));

                db.SaveChanges();
            }
        }
    }
}
