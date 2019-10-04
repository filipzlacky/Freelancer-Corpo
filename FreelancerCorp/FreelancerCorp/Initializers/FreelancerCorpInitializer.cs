using System;
using System.Data.Entity;

namespace FreelancerCorp.Initializers {
    public class FreelancerCorpInitializer : DropCreateDatabaseAlways<FreelancerCorpDbContext> {

        protected override void Seed(FreelancerCorpDbContext context) {
            context.FreeInfos.Add(new Entities.FreelancerInfo("Jozko Vajda", "Brno", "noob@gmail.com", new DateTime(2002, 2, 2), Enums.Sex.FEMALE));

            base.Seed(context);
        }
    }
}
