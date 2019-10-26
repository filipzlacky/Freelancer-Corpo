using System;
using System.Data.Entity;
using FreelancerCorp.DataAccessLayer.Entities;

namespace FreelancerCorp.DataAccessLayer.Initializers {
    public class FreelancerCorpInitializer : DropCreateDatabaseAlways<FreelancerCorpDbContext> {

        protected override void Seed(FreelancerCorpDbContext context) {
            context.Freelancers.Add(new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1955, 7, 31), "VajdaLand", "", "name: Jozko Vajda", ""));

            base.Seed(context);
        }
    }
}
