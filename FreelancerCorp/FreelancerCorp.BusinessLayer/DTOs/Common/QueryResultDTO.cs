using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Common
{
    public class QueryResultDTO<TDto, TFilter> where TFilter : FilterDTOBase
    {
        /// <summary>
        /// Total number of items for the query
        /// </summary>
        public long TotalItemsCount { get; set; }

        /// <summary>
        /// Number of page (indexed from 1) which was requested
        /// </summary>
        public int? RequestedPageNumber { get; set; }

        /// <summary>
        /// Size of the page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The query results page
        /// </summary>
        public IEnumerable<TDto> Items { get; set; }

        /// <summary>
        /// Applied filter for this query
        /// </summary>
        public TFilter Filter { get; set; }

        public override string ToString()
        {
            return $"{TotalItemsCount} {typeof(TDto).Name}(s)" +
                   $"{(RequestedPageNumber != null ? $", page {RequestedPageNumber}/{Math.Ceiling(TotalItemsCount / (double)PageSize)}." : ".")}";
        }

        public IEnumerable<TDto> PagedResult()
        {
            if (RequestedPageNumber.HasValue)
            {
                var index = (int)(RequestedPageNumber - 1) * PageSize;
                int itemsNum = 0;
                if (index + PageSize >= Items.Count())
                {
                    itemsNum = Items.Count() - index;
                } else
                {
                    itemsNum = PageSize;
                }
                return Items.ToList().GetRange((int)(RequestedPageNumber - 1) * PageSize, itemsNum);
            }
            return Items;
        }
    }
}
