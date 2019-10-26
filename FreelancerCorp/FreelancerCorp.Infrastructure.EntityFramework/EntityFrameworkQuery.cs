﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using FreelancerCorp.Infrastructure.EntityFramework.UnitOfWork;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using FreelancerCorp.Infrastructure.UnitOfWork;


namespace FreelancerCorp.Infrastructure.EntityFramework
{
    public class EntityFrameworkQuery<TEntity> : QueryBase<TEntity> where TEntity : class, IEntity, new()
    {
        protected DbContext Context => ((EntityFrameworkUnitOfWork)UOWProvider.GetUnitOfWorkInstance()).Context;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityFrameworkQuery{TResult}" /> class.
        /// </summary>
        public EntityFrameworkQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public override async Task<QueryResult<TEntity>> ExecuteAsync()
        {
            if (Predicate == null)
            {
                throw new ArgumentException("Predicate can't be null.");
            }
            
        }
    }
}