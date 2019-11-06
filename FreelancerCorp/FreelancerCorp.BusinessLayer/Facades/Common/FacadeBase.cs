using FreelancerCorp.Infrastructure.UnitOfWork;

namespace FreelancerCorp.BusinessLayer.Facades.Common
{
    public abstract class FacadeBase
    {
        protected readonly IUnitOfWorkProvider UnitOfWorkProvider;
        protected FacadeBase(IUnitOfWorkProvider unitOfWorkProvider)
        {
            UnitOfWorkProvider = unitOfWorkProvider;
        }
    }
}
