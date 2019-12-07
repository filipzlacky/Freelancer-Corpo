using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FreelancerCorp.DataAccessLayer;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.DataAccessLayer.Enums;
using FreelancerCorp.Infrastructure;
using FreelancerCorp.Infrastructure.EntityFramework;
using FreelancerCorp.Infrastructure.EntityFramework.UnitOfWork;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.UnitOfWork;

namespace FreelamcerCorp.DAL.Tests.Config
{
    class EntityFrameworkTestInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryTestFreelancerCorp";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityFrameworkQuery<>))
                    .LifestyleTransient()
            );
        }

        private static DbContext InitializeDatabase()
        {
            var context = new FreelancerCorpDbContext();
            //context.Users.RemoveRange(context.Users);
            //context.Users.RemoveRange(context.Users);
            //context.Users.RemoveRange(context.Users);
            //context.Offers.RemoveRange(context.Offers);
            //context.Ratings.RemoveRange(context.Ratings);
            //context.SaveChanges();

            //context.Users.Add(new Freelancer(Sex.MALE, new DateTime(1968, 12, 5), "Bardejov", "hookaj25@gmail.com", "Karol Kovach", "som super"));
            //context.Users.Add(new Freelancer(Sex.MALE, new DateTime(1954, 3, 9), "Brno", "serzant@gmail.com", "Pavel Mnich", "milujte jedlo"));
            //context.Users.Add(new Freelancer(Sex.TRANSGENDER, new DateTime(1997, 5, 15), "Liberec", "sunshine33@azet.cz", "Simon Sarafy", "nemam problem"));

            //context.Users.Add(new Corporation("Úzka 12, Zvolen 925 25", "greatcorp@gmail.com", "GreatCorp", "berieme kazdeho"));
            //context.Users.Add(new Corporation("Adresna 0, Niekde 999 99", "nekontaktuj@vaznenie.com", "NejakyCorp", "nechod sem"));
            //context.Users.Add(new Corporation("Adresna 0, Niekde 999 99", "uzzasevy@nieee.com", "NoCorp", "navzdy zatvorene"));

            //context.Offers.Add(new Offer(Category.GRAPHICS, "usable 3d model", 50, 1, "what", "but not so pretty"));

            //context.Ratings.Add(new Rating(0, 1, "what the hell"));

            context.SaveChanges();

            return context;
        }
    }
}
