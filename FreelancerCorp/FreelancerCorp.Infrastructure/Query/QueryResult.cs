using System.Collections.Generic;
using System.Linq;
using FreelancerCorp.Infrastructure;

namespace FreelancerCorp.Infrastructure.Query
{
    public class QueryResult<TEntity> where TEntity : IEntity
    {
        public long TotalItemsCount { get; }
        public int? RequestedPageNumber { get; }
        public int PageSize { get; }
        public IList<TEntity> Items { get; }

        public bool PagingEnabled => RequestedPageNumber != null;

        public QueryResult(IList<TEntity> items, long totalItemsCount, int pageSize = 10, int? requestedPageNumber = null)
        {
            TotalItemsCount = totalItemsCount;
            RequestedPageNumber = requestedPageNumber;
            PageSize = pageSize;
            Items = items;
        }

        public override bool Equals(object obj) {
            var result = obj as QueryResult<TEntity>;
            return result != null &&
                   TotalItemsCount == result.TotalItemsCount &&
                   RequestedPageNumber == result.RequestedPageNumber &&
                   PageSize == result.PageSize &&
                   EqualityComparer<IList<TEntity>>.Default.Equals(Items, result.Items) &&
                   PagingEnabled == result.PagingEnabled;
        }

        public override int GetHashCode() {
            var hashCode = -714620281;
            hashCode = hashCode * -1521134295 + TotalItemsCount.GetHashCode();
            hashCode = hashCode * -1521134295 + RequestedPageNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + PageSize.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<TEntity>>.Default.GetHashCode(Items);
            hashCode = hashCode * -1521134295 + PagingEnabled.GetHashCode();
            return hashCode;
        }
    }
}
