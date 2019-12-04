using System;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.DataAccessLayer.Enums;
using FreelancerCorp.DataAccessLayer;


namespace FreelancerCorp.Executable {
    class Program {
        static void Main(string[] args) {

            using (var db = new FreelancerCorpDbContext()) {

                db.Users.Add(new Freelancer(Sex.TRANSGENDER, new DateTime(1955, 7, 31), "VajdaLand", "", "Imre Gejza", ""));
                db.Users.Add(new Corporation("VajdaLand", "vajdajozko@vajdaland.cock", "Punch Job", ""));
                db.Users.Add(new Freelancer(Sex.TRANSGENDER, new DateTime(1955, 7, 31), "VajdaLand", "", "Jozko Vajda", ""));                

                db.SaveChanges();
            }
        }
    }
}
