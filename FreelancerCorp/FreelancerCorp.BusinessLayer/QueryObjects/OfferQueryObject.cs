using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using FreelancerCorp.Infrastructure.Query.Predicates;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;
using System.Collections.Generic;
using System.Linq;
using FreelancerCorp.BusinessLayer.DTOs;

namespace FreelancerCorp.BusinessLayer.QueryObjects
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

            if (filter.SearchedAppliersIds != null && filter.SearchedAppliersIds.Length != 0)
            {
                var predicate = new List<IPredicate>(filter.SearchedAppliersIds
                    .Select(authorId => new SimplePredicate(
                        nameof(Offer.ApplierId),
                        ValueComparingOperator.Equal,
                        authorId)));

                predicates.Add(new CompositePredicate(predicate));
            }

            if (!string.IsNullOrEmpty(filter.SearchedName))
            {
                predicates.Add(new SimplePredicate(nameof(Offer.Name), ValueComparingOperator.Equal, filter.SearchedName));
            }

            if (filter.SearchedCategory.HasValue)
            {
                predicates.Add(new SimplePredicate(nameof(Offer.Category), ValueComparingOperator.Equal, filter.SearchedCategory));
            }

            if (filter.SearchedPrice.HasValue)
            {
                predicates.Add(new SimplePredicate(nameof(Offer.Price), ValueComparingOperator.LessThanOrEqual, filter.SearchedPrice));
            }

            if (predicates.Count == 0)
            {
                return query;
            }

            return query.Where(new CompositePredicate(predicates));
        }

        protected override IQuery<Offer> GetAll(IQuery<Offer> query)
        {
            return query;
        }
    }
}
