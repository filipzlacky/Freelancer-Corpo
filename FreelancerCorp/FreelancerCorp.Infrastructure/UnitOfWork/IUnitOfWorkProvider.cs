using System;

namespace FreelancerCorp.Infrastructure.UnitOfWork {
    public interface IUnitOfWorkProvider : IDisposable {

        IUnitOfWork Create();
        
        IUnitOfWork GetUnitOfWorkInstance();
    }
}
