using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure;
using FreelancerCorp.Infrastructure.UnitOfWork;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelamcerCorp.DAL.Tests.RepositoryTests
{
    [TestFixture]
    public class CorporationRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<Corporation> corporationRepository = Initializer.Container.Resolve<IRepository<Corporation>>();

        [Test]
        public async Task GetCorporationAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectCorporation()
        {
            Corporation gotCorporation;

            using (unitOfWorkProvider.Create())
            {
                gotCorporation = await corporationRepository.GetAsync(2);
            }

            Assert.AreEqual(gotCorporation.Id, 2);
        }

        [Test]
        public async Task CreateCorporationAsync_CorporationIsNotPreviouslySeeded_CreatesNewCorporation()
        {
            var corp = new Corporation("somewhere 01, 12588 here", "yaaaay@tests.com", "justcorp", "we are the champions");

            using (var uow = unitOfWorkProvider.Create())
            {
                corporationRepository.Create(corp);
                await uow.Commit();

            }
            Assert.IsTrue(!corp.Id.Equals(Guid.Empty));
        }

        [Test]
        public async Task DeleteCategoryAsync_CategoryIsPreviouslySeeded_DeletesCategory()
        {
            Corporation delCorp;

            using (var uow = unitOfWorkProvider.Create())
            {
                corporationRepository.Delete(1);
                await uow.Commit();
                delCorp = await corporationRepository.GetAsync(1);
            }

            Assert.AreEqual(delCorp, null);
        }
    }
}
