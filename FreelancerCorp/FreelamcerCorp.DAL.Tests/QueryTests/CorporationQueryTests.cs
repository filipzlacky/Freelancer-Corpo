using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using FreelancerCorp.Infrastructure.UnitOfWork;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelamcerCorp.DAL.Tests.QueryTests
{
    [TestFixture]
    public class CorporationQueryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        [Test]
        public async Task ExecuteAsync_SimpleWherePredicate_ReturnsCorrectQueryResult()
        {
            QueryResult<Corporation> actualQueryResult;
            var categoryQuery = Initializer.Container.Resolve<IQuery<Corporation>>();
            var expectedQueryResult = new QueryResult<Corporation>(new List<Corporation>{
                new Corporation("Adresna 0, Niekde 999 99", "nekontaktuj@vaznenie.com", "NejakyCorp", "nechod sem"),
                new Corporation("Adresna 0, Niekde 999 99", "uzzasevy@nieee.com", "NoCorp", "navzdy zatvorene")
            }, 2);

            var predicate = new SimplePredicate(nameof(Corporation.Address), ValueComparingOperator.Equal, "Adresna 0, Niekde 999 99");
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await categoryQuery.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

    }
}
