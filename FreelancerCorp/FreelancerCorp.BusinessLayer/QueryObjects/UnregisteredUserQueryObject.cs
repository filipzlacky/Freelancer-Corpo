using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.QueryObjects
{
    public class UnregisteredUserQueryObject : QueryObjectBase<UnregisteredUserDTO, UnregisteredUser, UnregisteredUserFilterDTO, IQuery<UnregisteredUser>>
    {
        public UnregisteredUserQueryObject(IMapper mapper, IQuery<UnregisteredUser> unregisteredUser) : base(mapper, unregisteredUser) { }

        protected override IQuery<UnregisteredUser> ApplyWhereClause(IQuery<UnregisteredUser> query, UnregisteredUserFilterDTO filter)
        {
            var predicates = new List<IPredicate>();

            if (!string.IsNullOrEmpty(filter.SearchedLocation))
            {
                predicates.Add(new SimplePredicate(nameof(UnregisteredUser.Location), ValueComparingOperator.Equal, filter.SearchedLocation));
            }

            if (!string.IsNullOrEmpty(filter.SearchedEmail))
            {
                predicates.Add(new SimplePredicate(nameof(UnregisteredUser.Email), ValueComparingOperator.Equal, filter.SearchedEmail));
            }

            if (!string.IsNullOrEmpty(filter.SearchedName))
            {
                predicates.Add(new SimplePredicate(nameof(UnregisteredUser.Name), ValueComparingOperator.Equal, filter.SearchedName));
            }

            predicates.Add(new SimplePredicate(nameof(UnregisteredUser.UserRole), ValueComparingOperator.Equal, filter.UserRole));

            return query.Where(new CompositePredicate(predicates));
        }

        protected override IQuery<UnregisteredUser> GetAll(IQuery<UnregisteredUser> query)
        {
            return query;
        }
    }
}
