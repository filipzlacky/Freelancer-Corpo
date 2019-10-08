using System.Collections.Generic;
using System.Linq;
using FreelancerCorp.Infrastructure;

namespace FreelancerCorp.Infrastructure.Query
{
    public class QueryResult<TEntity> where TEntity : IEntity
    {
        public long TotalItemsCount { get; set; }
        public int RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public IList<TEntity> Items { get; set; }
        public bool PagingEnabled { get; set; }

        public QueryResult(long totalItemsCount, int requestedPageNumber, int pageSize, IList<TEntity> items, bool pagingEnabled) {
            TotalItemsCount = totalItemsCount;
            RequestedPageNumber = requestedPageNumber;
            PageSize = pageSize;
            Items = items;
            PagingEnabled = pagingEnabled;
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
