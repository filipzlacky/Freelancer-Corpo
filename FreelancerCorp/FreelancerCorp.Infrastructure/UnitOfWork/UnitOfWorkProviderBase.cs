using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FreelancerCorp.Infrastructure.UnitOfWork {
    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider {
        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance = new AsyncLocal<IUnitOfWork>();

        public abstract IUnitOfWork Create();

        public void Dispose() {
            UowLocalInstance.Value?.Dispose();
            UowLocalInstance.Value = null;
        }

        public IUnitOfWork GetUnitOfWorkInstance() {
            return UowLocalInstance != null ? UowLocalInstance.Value : throw new InvalidOperationException("Uow not created");
        }
    }
}
