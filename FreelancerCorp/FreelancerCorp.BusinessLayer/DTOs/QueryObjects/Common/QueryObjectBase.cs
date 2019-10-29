using System;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.Infrastructure;
using FreelancerCorp.Infrastructure.Query;

namespace FreelancerCorp.BusinessLayer.QueryObjects.Common
{
    public abstract class QueryObjectBase<TDto, TEntity, TFilter, TQuery>
        where TFilter : FilterDTOBase
        where TQuery : IQuery<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IMapper mapper;

        protected readonly IQuery<TEntity> Query;

        protected QueryObjectBase(IMapper mapper, TQuery query)
        {
            this.mapper = mapper;
            this.Query = query;
        }

        protected abstract IQuery<TEntity> ApplyWhereClause(IQuery<TEntity> query, TFilter filter);

        public virtual async Task<QueryResultDTO<TDto, TFilter>> ExecuteQuery(TFilter filter)
        {
            var query = ApplyWhereClause(Query, filter);

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query = query.SortBy(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }
            var queryResult = await query.ExecuteAsync();

            var queryResultDTO = mapper.Map<QueryResultDTO<TDto, TFilter>>(queryResult);
            queryResultDTO.Filter = filter;

            return queryResultDTO;
        }
    }
}
