using System.Data.Common;
using System.Data.Entity;
using FreelancerCorp.Entities;

namespace FreelancerCorp {
    public class FreelancerCorpDbContext : DbContext {
        public DbSet<IUser> Users { get; set; }
        public DbSet<IOffer> Offers { get; set; }
        public DbSet<IGeneralInfo> GeneralInfos { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        public FreelancerCorpDbContext() {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public FreelancerCorpDbContext(DbConnection connection) : base (connection, true) {
            Database.CreateIfNotExists();
        }

    }
}
