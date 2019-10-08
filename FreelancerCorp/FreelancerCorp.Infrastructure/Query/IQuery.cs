﻿using FreelancerCorp.Infrastructure;
using System.Threading.Tasks;
using FreelancerCorp.Infrastructure.Query.Predicates;

namespace FreelancerCorp.Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IQuery<TEntity> Where(IPredicate rootPredicate);

        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IQuery<TEntity> SortBy(string sortAccordingTo, bool ascendingOrder = true);

        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        /// <summary>
        /// Executes the query and returns the results.
        /// </summary>
        Task<QueryResult<TEntity>> ExecuteAsync();
    }
}
