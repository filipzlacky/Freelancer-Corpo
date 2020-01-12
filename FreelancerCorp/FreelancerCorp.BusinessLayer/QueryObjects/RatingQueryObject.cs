using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using System;
using AutoMapper;
using System.Collections.Generic;
using FreelancerCorp.Infrastructure.Query.Predicates;
using System.Linq;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using FreelancerCorp.BusinessLayer.DTOs;

namespace FreelancerCorp.BusinessLayer.QueryObjects
{
    public class RatingQueryObject : QueryObjectBase<RatingDTO, Rating, RatingFilterDTO, IQuery<Rating>>
    {
        public RatingQueryObject(IMapper mapper, IQuery<Rating> rating) : base(mapper, rating) { }

        protected override IQuery<Rating> ApplyWhereClause(IQuery<Rating> query, RatingFilterDTO filter)
        {
            var predicates = new List<IPredicate>();

            if (filter.SearchedRatedUsersId != null && filter.SearchedRatedUsersId.Length != 0)
            {
                var predicate = new List<IPredicate>(filter.SearchedRatedUsersId
                    .Select(searchedUserId => new SimplePredicate(nameof(Rating.RatedUserId), ValueComparingOperator.Equal, searchedUserId)));

                predicates.Add(new CompositePredicate(predicate));
            }
            if (filter.SearchedScore.HasValue)
            {
                predicates.Add(new SimplePredicate(nameof(Rating.Score), ValueComparingOperator.GreaterThanOrEqual, filter.SearchedScore.Value));
            }

            return query.Where(new CompositePredicate(predicates));
        }

        protected override IQuery<Rating> GetAll(IQuery<Rating> query)
        {
            return query;
        }
    }
}
