using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using System.Linq;
using System.Collections.Generic;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;

namespace FreelancerCorp.BusinessLayer.DTOs.QueryObjects
{
    public class CorporationQueryObject : QueryObjectBase<CorporationDTO, Corporation, CorporationFilterDTO, IQuery<Corporation>>
    {

        public CorporationQueryObject(IMapper mapper, IQuery<Corporation> offer) : base(mapper, offer) { }

        protected override IQuery<Corporation> ApplyWhereClause(IQuery<Corporation> query, CorporationFilterDTO filter)
        {
            var predicates = new List<IPredicate>();

            if (!string.IsNullOrEmpty(filter.SearchedLocation))
            {
                predicates.Add(new SimplePredicate(nameof(Corporation.Address), ValueComparingOperator.Equal, filter.SearchedLocation));
            }

            if (filter.CorporationNames != null && filter.CorporationNames.Length != 0)
            {
                var predicate = new List<IPredicate>(filter.CorporationNames
                .Select(name => new SimplePredicate(
                    nameof(Corporation.Name),
                    ValueComparingOperator.Equal,
                    name)));

                predicates.Add(new CompositePredicate(predicate));
            }

            return query.Where(new CompositePredicate(predicates));
        }
    }
}
