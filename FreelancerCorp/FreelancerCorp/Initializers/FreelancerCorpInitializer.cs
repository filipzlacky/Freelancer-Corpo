using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using FreelancerCorp.DataAccessLayer.Entities;

namespace FreelancerCorp.DataAccessLayer.Initializers {
    public class FreelancerCorpInitializer : DropCreateDatabaseAlways<FreelancerCorpDbContext> {

        protected override void Seed(FreelancerCorpDbContext context) {
            //context.Freelancers.Add(new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1955, 8, 31), "VajLand", "", "Jozko Vajda", ""));

            context.Users.AddOrUpdate(new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1955, 8, 31), "VajLand", "", "Jozko Vajda", ""));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
