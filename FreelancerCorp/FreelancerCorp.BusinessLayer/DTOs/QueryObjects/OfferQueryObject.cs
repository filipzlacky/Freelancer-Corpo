using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using System.Collections.Generic;
using System.Linq;

namespace FreelancerCorp.BusinessLayer.DTOs.QueryObjects
{
    public class OfferQueryObject : QueryObjectBase<OfferDTO, Offer, OfferFilterDTO, IQuery<Offer>>
    {
        public OfferQueryObject(IMapper mapper, IQuery<Offer> offer) :base(mapper, offer) { }

        protected override IQuery<Offer> ApplyWhereClause(IQuery<Offer> query, OfferFilterDTO filter)
        {
            var predicates = new List<IPredicate>();

            if (filter.SearchedAuthorsIds != null && filter.SearchedAuthorsIds.Length != 0)
            {
                var predicate = new List<IPredicate>(filter.SearchedAuthorsIds
                    .Select(authorId => new SimplePredicate(
                        nameof(Offer.CreatorId),
                        ValueComparingOperator.Equal, 
                        authorId)));

                predicates.Add(new CompositePredicate(predicate));
            }

            if (filter.SearchedCategory.HasValue)
            {
                predicates.Add(new SimplePredicate(nameof(Offer.Category), ValueComparingOperator.Equal, filter.SearchedCategory));
            }

            if (filter.SearchedPrice.HasValue)
            {
                predicates.Add(new SimplePredicate(nameof(Offer.Price), ValueComparingOperator.Equal, filter.SearchedPrice));
            }

            return query.Where(new CompositePredicate(predicates));
        }
    }
}
