using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelancerCorp.BusinessLayer.DTOs.QueryObjects
{
    public class FreelancerQueryObject : QueryObjectBase<FreelancerDTO, Freelancer, FreelancerFilterDTO, IQuery<Freelancer>>
    {
        public FreelancerQueryObject(IMapper mapper, IQuery<Freelancer> offer) : base(mapper, offer) { }

        protected override IQuery<Freelancer> ApplyWhereClause(IQuery<Freelancer> query, FreelancerFilterDTO filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrEmpty(filter.SearchedLocation))
            {
                predicates.Add(new SimplePredicate(nameof(Freelancer.Location), ValueComparingOperator.Equal, filter.SearchedLocation));
            }

            if (filter.SearchedFreelancerNames != null && filter.SearchedFreelancerNames.Length != 0)
            {
                var predicate = new List<IPredicate>(filter.SearchedFreelancerNames
                .Select(name => new SimplePredicate(
                    nameof(Freelancer.Name),
                    ValueComparingOperator.Equal,
                    name)));

                predicates.Add(new CompositePredicate(predicate));
            }

            return query.Where(new CompositePredicate(predicates));
        }
    }
}
