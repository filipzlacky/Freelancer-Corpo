using System;
using System.Linq;
using System.Threading.Tasks;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.UnitOfWork;

namespace FreelancerCorp.Infrastructure.Query
{
    public abstract class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class, IEntity, new() {
        public int PageSize { get; private set; }
        public int? DesiredPage { get; private set; }
        public string SortAccordingTo { get; set; }
        public bool UseAscendingOrder { get; set; }
        public IPredicate Predicate { get; set; }
        protected IUnitOfWorkProvider UOWProvider { get; set; }
        private int DefaultPageSize { get; set; }

        protected QueryBase(IUnitOfWorkProvider provider)
        {
            UOWProvider = provider;
        }

        protected QueryBase(int pageSize, int desiredPage, string sortAccordingTo, bool useAscendingOrder, IPredicate predicate, IUnitOfWorkProvider uOWProvider, int defaultPageSize) {
            PageSize = pageSize;
            DesiredPage = desiredPage;
            SortAccordingTo = sortAccordingTo;
            UseAscendingOrder = useAscendingOrder;
            Predicate = predicate;
            UOWProvider = uOWProvider;
            DefaultPageSize = defaultPageSize;
        }

        public IQuery<TEntity> Where(IPredicate rootPredicate) {
            if (rootPredicate == null) throw new ArgumentException("Root predicate can't be null");
            Predicate = rootPredicate;
            return this;
        }

        public IQuery<TEntity> SortBy(string sortAccordingTo, bool ascendingOrder = true) {
            if (sortAccordingTo == null) throw new ArgumentException("Sort according to can't be null");
            SortAccordingTo = sortAccordingTo;
            UseAscendingOrder = ascendingOrder;
            return this;
        }

        public IQuery<TEntity> Page(int pageToFetch, int pageSize = 10) {
            if (pageToFetch < 0 || pageSize < 0) throw new ArgumentException("Page size/ page to fetch can't be negative");

            DesiredPage = pageToFetch;
            PageSize = pageSize;
            return this;
        }

        public abstract Task<QueryResult<TEntity>> ExecuteAsync();
    }
}
