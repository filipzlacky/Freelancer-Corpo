using System;
using System.Data.Common;
using System.Data.Entity;
using FreelancerCorp.Entities;

namespace FreelancerCorp {
    public class FreelancerCorpDbContext : DbContext {

        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Corporation> Corporations { get; set; }       
        public DbSet<FreelancerInfo> FreeInfos { get; set; }
        public DbSet<CorporationInfo> CorpInfos { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        private const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=FreelanceCorpDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";

        public FreelancerCorpDbContext() : base (ConnectionString) {           
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;            
        }

        public FreelancerCorpDbContext(DbConnection connection) : base (connection, true) {
            Database.CreateIfNotExists();
        }

    }
}
