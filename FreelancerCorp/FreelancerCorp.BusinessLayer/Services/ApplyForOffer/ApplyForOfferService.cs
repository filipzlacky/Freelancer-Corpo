using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.Services.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure;

namespace FreelancerCorp.BusinessLayer.Services.ApplyForOffer
{
    public class ApplyForOfferService : ServiceBase, IApplyForOfferService
    {
        private readonly IRepository<Offer> offerRepository;

        public ApplyForOfferService(IMapper mapper, IRepository<Offer> repository)
            : base(mapper)
        {
            offerRepository = repository;
        }

        public async Task<int> ApplyUserForOffer(UserAppliesForOfferDTO uafoDTO)
        {
            var offer = await offerRepository.GetAsync(uafoDTO.Offer.Id);
            Mapper.Map(uafoDTO, offer);
            offer.ApplierId = uafoDTO.ApplierId;
            offer.ApplierRole = uafoDTO.ApplierRole.ToString();

            offerRepository.Update(offer);

            return offer.Id;
        }
    }
}
