using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserDTO>().ReverseMap();

            config.CreateMap<User, UserCreateFreelancerDTO>().ReverseMap();
            config.CreateMap<Freelancer, UserCreateFreelancerDTO>().ReverseMap();

            config.CreateMap<User, UserCreateCorporationDTO>().ReverseMap();
            config.CreateMap<Corporation, UserCreateCorporationDTO>().ReverseMap();

            config.CreateMap<Freelancer, FreelancerDTO>().ReverseMap();
            config.CreateMap<Offer, OfferDTO>().ReverseMap();
            config.CreateMap<Corporation, CorporationDTO>().ReverseMap();
            config.CreateMap<Rating, RatingDTO>().ReverseMap();

            config.CreateMap<User, UnregisteredUserDTO>().ReverseMap();
            config.CreateMap<UnregisteredUser, UnregisteredUserDTO>().ReverseMap();

            config.CreateMap<QueryResult<Freelancer>, QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>>();
            config.CreateMap<QueryResult<Corporation>, QueryResultDTO<CorporationDTO, CorporationFilterDTO>>();
            config.CreateMap<QueryResult<Offer>, QueryResultDTO<OfferDTO, OfferFilterDTO>>();
            config.CreateMap<QueryResult<Rating>, QueryResultDTO<RatingDTO, RatingFilterDTO>>();
            config.CreateMap<QueryResult<User>, QueryResultDTO<UserDTO, UserFilterDTO>>();
            config.CreateMap<QueryResult<UnregisteredUser>, QueryResultDTO<UnregisteredUserDTO, UnregisteredUserFilterDTO>>();
        }
    }
}
