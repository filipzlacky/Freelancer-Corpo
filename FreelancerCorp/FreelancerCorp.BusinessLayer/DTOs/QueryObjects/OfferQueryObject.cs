using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;

namespace FreelancerCorp.BusinessLayer.DTOs.QueryObjects
{
    public class OfferQueryObject : QueryObjectBase<OfferDTO, Offer, OfferFilterDTO, IQuery<Offer>>
    {
        public OfferQueryObject(IMapper mapper, IQuery<Offer> offer) :base(mapper, offer) { }

        protected override IQuery<Offer> ApplyWhereClause(IQuery<Offer> query, OfferFilterDTO filter)
        {
            throw new System.NotImplementedException();
        }
    }
}
