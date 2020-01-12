using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using System.Linq;
using System.Collections.Generic;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.QueryObjects
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

            if (!string.IsNullOrEmpty(filter.SearchedCorporationName))
            {
                predicates.Add(new SimplePredicate(nameof(Corporation.Name), ValueComparingOperator.Equal, filter.SearchedCorporationName));
            }

            predicates.Add(new SimplePredicate(nameof(Corporation.UserRole), ValueComparingOperator.Equal, filter.UserRole));

            return query.Where(new CompositePredicate(predicates));
        }

        public override async Task<QueryResultDTO<CorporationDTO, CorporationFilterDTO>> ExecuteEmptyQuery()
        {
            var query = GetAll(Query, new CorporationFilterDTO());

            var queryResult = await query.ExecuteAsync();

            var queryResultDTO = mapper.Map<QueryResultDTO<CorporationDTO, CorporationFilterDTO>>(queryResult);

            return queryResultDTO;
        }

        private IQuery<Corporation> GetAll(IQuery<Corporation> query, CorporationFilterDTO filter)
        {
            return query.Where(new SimplePredicate(nameof(Corporation.UserRole), ValueComparingOperator.Equal, filter.UserRole));
        }

        protected override IQuery<Corporation> GetAll(IQuery<Corporation> query)
        {
            return query;
        }
    }
}
