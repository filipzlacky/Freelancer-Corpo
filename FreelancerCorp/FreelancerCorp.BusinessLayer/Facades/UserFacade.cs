using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.Infrastructure.UnitOfWork;
using FreelancerCorp.BusinessLayer.Services.Freelancers;
using FreelancerCorp.BusinessLayer.Services.Corporations;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class UserFacade : FacadeBase
    {
        private readonly IFreelancerService freelancerService;
        private readonly ICorporationService corporationService;
        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IFreelancerService freelancer, ICorporationService corporation) 
            : base(unitOfWorkProvider)
        {
            freelancerService = freelancer;
            corporationService = corporation;
        }

        public async Task<int> CreateFreelancerAsync(FreelancerDTO freelancer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                int freelancerId = freelancerService.Create(freelancer);
                await uow.Commit();

                return freelancerId;
            }
        }

        public async Task<bool> EditFreelancerAsync(FreelancerDTO freelancer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await freelancerService.GetAsync(freelancer.Id, false)) == null)
                {
                    return false;
                }
                await freelancerService.Update(freelancer);
                await uow.Commit();

                return true;
            }
        }

        public async Task<bool> DeleteFreelancerAsync(int id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await freelancerService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                freelancerService.Delete(id);
                await uow.Commit();

                return true;
            }
        }

        public async Task<int> CreateCorporationAsync(CorporationDTO corporation)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                int corporationId = corporationService.Create(corporation);
                await uow.Commit();

                return corporationId;
            }
        }

        public async Task<bool> EditCorporationAsync(CorporationDTO corporation)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await corporationService.GetAsync(corporation.Id, false)) == null)
                {
                    return false;
                }
                await corporationService.Update(corporation);
                await uow.Commit();

                return true;
            }
        }

        public async Task<bool> DeleteCorporationAsync(int id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await corporationService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                corporationService.Delete(id);
                await uow.Commit();

                return true;
            }
        }

        public async Task<FreelancerDTO> GetFreelancerAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await freelancerService.GetAsync(id);
            }
        }

        public async Task<IEnumerable<int>> GetFreelancersAsync(params string[] names)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await freelancerService.GetFreelancerIdsByNamesAsync(names);
            }
        }

        public async Task<IEnumerable<int>> GetFreelancersAsync(string location)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await freelancerService.GetFreelancerIdsByLocationAsync(location);
            }
        }

        public async Task<IEnumerable<int>> GetFreelancersAsync(string location, params string[] names)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await freelancerService.GetFreelancerIdsByAllAsync(location, names);
            }
        }

        public async Task<IEnumerable<FreelancerDTO>> GetFreelancersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await freelancerService.ListAllAsync()).Items;
            }
        }

        public async Task<CorporationDTO> GetCorporationAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await corporationService.GetAsync(id);
            }
        }

        public async Task<IEnumerable<int>> GetCorporationsAsync(params string[] names)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await corporationService.GetCorporationIdsByNamesAsync(names);
            }
        }

        public async Task<IEnumerable<int>> GetCorporationsAsync(string location)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await corporationService.GetCorporationIdsByLocationAsync(location);
            }
        }

        public async Task<IEnumerable<int>> GetCorporationsAsync(string location, params string[] names)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await corporationService.GetCorporationIdsByAllAsync(location, names);
            }
        }

        public async Task<IEnumerable<CorporationDTO>> GetCorporationsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await corporationService.ListAllAsync()).Items;
            }
        }

        public double GetFreelancerAverageRating(FreelancerDTO freelancer)
        {
            return freelancer.Ratings.Average(rating => rating.Score);
        }
        public double GetCorporationAverageRating(CorporationDTO corporation)
        {
            return corporation.Ratings.Average(rating => rating.Score);
        }

        public int GetAge(FreelancerDTO freelancer)
        {
            return (DateTime.Now - freelancer.DoB).Days/365;
        }
    }
}
