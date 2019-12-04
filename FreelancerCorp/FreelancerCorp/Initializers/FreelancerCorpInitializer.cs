using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using FreelancerCorp.DataAccessLayer.Entities;

namespace FreelancerCorp.DataAccessLayer.Initializers {
    public class FreelancerCorpInitializer : DropCreateDatabaseAlways<FreelancerCorpDbContext> {

        protected override void Seed(FreelancerCorpDbContext context) {
            context.Freelancers.AddOrUpdate(new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1955, 8, 31), "VajLand", "", "Jozko Vajda", ""));
            //context.Users.AddOrUpdate(new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1955, 8, 31), "VajLand", "", "Jozko Vajda", ""));
            context.Corporations.AddOrUpdate(new Corporation("Vajdovo namestie 47", "vajdajozko@vajdo.sk", "VajdaCorp", ""));
            //context.Users.AddOrUpdate(new Corporation("Vajdovo namestie 47", "vajdajozko@vajdo.sk", "VajdaCorp", ""));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
