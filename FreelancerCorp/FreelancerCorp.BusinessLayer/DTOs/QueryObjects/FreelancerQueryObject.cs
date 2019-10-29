using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using System;

namespace FreelancerCorp.BusinessLayer.DTOs.QueryObjects
{
    public class FreelancerQueryObject : QueryObjectBase<FreelancerDTO, Freelancer, FreelancerFilterDTO, IQuery<Freelancer>>
    {
        protected override IQuery<Freelancer> ApplyWhereClause(IQuery<Freelancer> query, FreelancerFilterDTO filter)
        {
            throw new NotImplementedException();
        }
    }
}
