using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Services.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure;

namespace FreelancerCorp.BusinessLayer.Services.CreateRatings
{
    public class CreateRatingService : ServiceBase, ICreateRatingService
    {
        private readonly IRepository<Rating> ratingRepository;

        public CreateRatingService(IMapper mapper, IRepository<Rating> ratingRepository)
            : base(mapper)
        {
            this.ratingRepository = ratingRepository;
        }

        //
        public int CreateNewRating(CreateRatingDTO createRatingDTO)
        {
            var rating = Mapper.Map<Rating>(createRatingDTO.Rating);
            rating.CreatorId = createRatingDTO.CreatorId;
            ratingRepository.Create(rating);
            return rating.Id;
        }
    }
}
