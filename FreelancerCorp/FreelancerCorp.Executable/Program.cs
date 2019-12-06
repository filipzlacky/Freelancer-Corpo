using System;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.DataAccessLayer.Enums;
using FreelancerCorp.DataAccessLayer;
using System.Linq;

namespace FreelancerCorp.Executable {
    class Program {
        static void Main(string[] args) {

            using (var db = new FreelancerCorpDbContext()) {

                db.Freelancers.Add(new Freelancer(Sex.TRANSGENDER, new DateTime(1955, 7, 31), "VajdaLand", "", "Imre Gejza", ""));
                db.Corporations.Add(new Corporation("VajdaLand", "vajdajozko@vajdaland.cock", "Punch Job", ""));
                db.Freelancers.Add(new Freelancer(Sex.TRANSGENDER, new DateTime(1955, 7, 31), "VajdaLand", "", "Jozko Vajda", ""));

                var x = db.Freelancers.FirstOrDefault();
                Console.WriteLine(x.GetType());
                Console.ReadKey();

                db.SaveChanges();
            }
        }
    }
}
