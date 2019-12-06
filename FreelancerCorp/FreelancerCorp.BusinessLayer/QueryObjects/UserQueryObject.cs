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
    public class UserQueryObject : QueryObjectBase<UserDTO, User, UserFilterDTO, IQuery<User>>
    {
        public UserQueryObject(IMapper mapper, IQuery<User> query) : base(mapper, query)
        {
        }

        protected override IQuery<User> ApplyWhereClause(Infrastructure.Query.IQuery<User> query, UserFilterDTO filter)
        {
            return query.Where(new SimplePredicate(nameof(User.UserName), ValueComparingOperator.Equal, filter.UserName));
        }
        

        protected override IQuery<User> GetAll(IQuery<User> query)
        {
            return query;
        }

        //protected override IQuery<User> GetAll(IQuery<User> query, UserFilterDTO filter)
        //{
        //    return query;
        //}
    }
}
