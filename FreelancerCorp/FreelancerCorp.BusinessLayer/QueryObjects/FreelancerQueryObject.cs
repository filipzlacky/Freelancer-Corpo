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
using FreelancerCorp.BusinessLayer.DTOs;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.QueryObjects
{
    public class FreelancerQueryObject : QueryObjectBase<FreelancerDTO, Freelancer, FreelancerFilterDTO, IQuery<Freelancer>>
    {
        public FreelancerQueryObject(IMapper mapper, IQuery<Freelancer> offer) : base(mapper, offer) { }

        protected override IQuery<Freelancer> ApplyWhereClause(IQuery<Freelancer> query, FreelancerFilterDTO filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            predicates.Add(new SimplePredicate(nameof(Freelancer.UserRole), ValueComparingOperator.Equal, filter.UserRole));

            if (!string.IsNullOrEmpty(filter.SearchedLocation))
            {
                predicates.Add(new SimplePredicate(nameof(Freelancer.Location), ValueComparingOperator.Equal, filter.SearchedLocation));
            }

            if (!string.IsNullOrEmpty(filter.SearchedSex))
            {
                predicates.Add(new SimplePredicate(nameof(Freelancer.Sex), ValueComparingOperator.Equal, filter.SearchedSex));
            }

            if (!string.IsNullOrEmpty(filter.SearchedUserName))
            {
                predicates.Add(new SimplePredicate(nameof(Freelancer.UserName), ValueComparingOperator.Equal, filter.SearchedUserName));
            }

            if (!string.IsNullOrEmpty(filter.SearchedFreelancerName))
            {
                predicates.Add(new SimplePredicate(nameof(Freelancer.Name), ValueComparingOperator.Equal, filter.SearchedFreelancerName));
            }

            return query.Where(new CompositePredicate(predicates));
        }

        public override async Task<QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>> ExecuteEmptyQuery()
        {
            var query = GetAll(Query, new FreelancerFilterDTO());

            var queryResult = await query.ExecuteAsync();

            var queryResultDTO = mapper.Map<QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>>(queryResult);

            return queryResultDTO;
        }

        protected IQuery<Freelancer> GetAll(IQuery<Freelancer> query, FreelancerFilterDTO filter)
        {
            return query.Where(new SimplePredicate(nameof(Freelancer.UserRole), ValueComparingOperator.Equal, filter.UserRole));
        }

        protected override IQuery<Freelancer> GetAll(IQuery<Freelancer> query)
        {
            return query;
        }
    }
}
