using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.ApplyForOffer
{
    interface IApplyForOfferService
    {
        Task<int> ApplyUserForOffer(UserAppliesForOfferDTO uafoDTO);
    }
}
