using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using System;

namespace FreelancerCorp.BusinessLayer.DTOs.QueryObjects
{
    public class CorporationQueryObject : QueryObjectBase<CorporationDTO, Corporation, CorporationFilterDTO, IQuery<Corporation>>
    {

        public CorporationQueryObject(IMapper mapper, IQuery<Corporation> offer) : base(mapper, offer) { }

        protected override IQuery<Corporation> ApplyWhereClause(IQuery<Corporation> query, CorporationFilterDTO filter)
        {
            throw new NotImplementedException();
        }
    }
}
