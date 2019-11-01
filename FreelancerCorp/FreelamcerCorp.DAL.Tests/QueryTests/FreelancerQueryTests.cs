using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.DataAccessLayer.Enums;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using FreelancerCorp.Infrastructure.UnitOfWork;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelamcerCorp.DAL.Tests.QueryTests
{
    [TestFixture]
    class FreelancerQueryTests
    {

        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        [Test]
        public async Task ExecuteAsync_ComplexWherePredicate_ReturnsCorrectQueryResult()
        {
            QueryResult<Freelancer> actualQueryResult;
            var categoryQuery = Initializer.Container.Resolve<IQuery<Freelancer>>();
            var expectedQueryResult = new QueryResult<Freelancer>(new List<Freelancer> {
                new Freelancer(Sex.MALE, new DateTime(1968, 12, 5), "Bardejov", "hookaj25@gmail.com", "Karol Kovach", "som super")
        }, 1);

            var predicate = new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(Freelancer.Sex), ValueComparingOperator.Equal, Sex.MALE),
                new CompositePredicate(new List<IPredicate>
                {
                    new SimplePredicate(nameof(Freelancer.Location), ValueComparingOperator.Equal, "Brno"),
                    new SimplePredicate(nameof(Freelancer.Location), ValueComparingOperator.Equal, "Liberec")
                }, LogicalOperator.OR)
            });
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await categoryQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

        [Test]
        public async Task ExecuteAsync_OrderAllCategoriesByName_ReturnsCorrectlyOrderedQueryResult()
        {
            QueryResult<Freelancer> actualQueryResult;
            var categoryQuery = Initializer.Container.Resolve<IQuery<Freelancer>>();
            var expectedQueryResult = new QueryResult<Freelancer>(new List<Freelancer>{
                new Freelancer(Sex.MALE, new DateTime(1954, 3, 9), "Brno", "serzant@gmail.com", "Pavel Mnich", "milujte jedlo"),
                new Freelancer(Sex.TRANSGENDER, new DateTime(1997, 5, 15), "Liberec", "sunshine33@azet.cz", "Simon Sarafy", "nemam problem"),
                new Freelancer(Sex.MALE, new DateTime(1968, 12, 5), "Bardejov", "hookaj25@gmail.com", "Karol Kovach", "som super")
            }, 3);

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await categoryQuery.SortBy(nameof(Freelancer.Info), true).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

    }
}
