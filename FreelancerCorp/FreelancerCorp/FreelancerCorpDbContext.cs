using System;
using System.Data.Common;
using System.Data.Entity;
using FreelancerCorp.DataAccessLayer.Initializers;
using FreelancerCorp.DataAccessLayer.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FreelancerCorp.DataAccessLayer {
    public class FreelancerCorpDbContext : DbContext {

        public DbSet<UnregisteredUser> UnregisteredUsers { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Corporation> Corporations { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Offer> Offers { get; set; }


        //private const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=FreelanceCorpDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
        private const string ConnectionString = "Server=tcp:ultimatefreelancercorpdbserver.database.windows.net,1433;Initial Catalog=FreelancerCorpDB;Persist Security Info=True;User ID=FreelancerCorpAdmin;Password=Fr33lanc3rC0rp;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        public FreelancerCorpDbContext() : base (ConnectionString) {
            Database.SetInitializer(new FreelancerCorpInitializer());
        }

        public FreelancerCorpDbContext(DbConnection connection) : base (connection, true) {
            Database.CreateIfNotExists();
        }

        override protected void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();                     
        }
    }
}
