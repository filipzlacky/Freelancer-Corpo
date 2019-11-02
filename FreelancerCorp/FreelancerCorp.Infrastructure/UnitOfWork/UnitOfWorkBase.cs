using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.Infrastructure.UnitOfWork {
    public abstract class UnitOfWorkBase : IUnitOfWork {
        private IList<Action> afterCommitActions = new List<Action>();

        public async Task Commit() {

            await CommitCore();
            foreach (var action in afterCommitActions) {
                action();
            }
            afterCommitActions.Clear();
        }

        public abstract void Dispose();

        public void RegisterAction(Action action) {
            afterCommitActions.Add(action);
        }

        protected abstract Task CommitCore();
    }
}
