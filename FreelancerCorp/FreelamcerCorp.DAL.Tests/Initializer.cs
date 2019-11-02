using Castle.Windsor;
using FreelamcerCorp.DAL.Tests.Config;
using FreelancerCorp.DataAccessLayer;
using NUnit.Framework;
using System.Data.Entity;

namespace FreelamcerCorp.DAL.Tests
{
    [SetUpFixture]
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<FreelancerCorpDbContext>());
            Container.Install(new EntityFrameworkTestInstaller());
        }
    }
}
