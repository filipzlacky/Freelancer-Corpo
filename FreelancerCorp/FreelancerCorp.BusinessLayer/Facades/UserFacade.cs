using FreelancerCorp.BusinessLayer.Facades.Common;
using FreelancerCorp.Infrastructure.UnitOfWork;
using FreelancerCorp.BusinessLayer.Services.Freelancers;
using FreelancerCorp.BusinessLayer.Services.Corporations;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Services.Users;
using FreelancerCorp.BusinessLayer.Services.Unregistered;

namespace FreelancerCorp.BusinessLayer.Facades
{
    public class UserFacade : FacadeBase
    {
        private readonly IFreelancerService freelancerService;
        private readonly ICorporationService corporationService;
        private readonly IUnregisteredService unregisteredService;
        private readonly IUserService userService;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IFreelancerService freelancer, ICorporationService corporation, IUnregisteredService unregistered, IUserService user) 
            : base(unitOfWorkProvider)
        {
            freelancerService = freelancer;
            corporationService = corporation;
            unregisteredService = unregistered;
            userService = user;
        }

        #region CRUD
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

        public async Task<int> CreateUnregisteredAsync(UnregisteredUserDTO unregistered)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                int unregisteredId = unregisteredService.Create(unregistered);
                await uow.Commit();

                return unregisteredId;
            }
        }

        public async Task<bool> EditUnregisteredAsync(UnregisteredUserDTO unregistered)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await unregisteredService.GetAsync(unregistered.Id, false)) == null)
                {
                    return false;
                }
                await unregisteredService.Update(unregistered);
                await uow.Commit();

                return true;
            }
        }

        public async Task<bool> DeleteUnregisteredAsync(int id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await unregisteredService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                unregisteredService.Delete(id);
                await uow.Commit();

                return true;
            }
        }

        public async Task<UserDTO> GetUserAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetUserAccordingToIdAsync(id);
            }
        }

        public async Task<FreelancerDTO> GetFreelancerAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await freelancerService.GetAsync(id);
            }
        }

        public async Task<CorporationDTO> GetCorporationAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await corporationService.GetAsync(id);
            }
        }

        
        public async Task<UnregisteredUserDTO> GetUnregisteredAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await unregisteredService.GetAsync(id);
            }
        }
        

        #endregion

        #region Accounts
        public async Task<int> RegisterFreelancer(UserCreateFreelancerDTO userDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await userService.RegisterFreelancerUserAsync(userDto);
                    await uow.Commit();
                    return id;
                } catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task<int> RegisterCorporation(UserCreateCorporationDTO userDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var id = await userService.RegisterCorporationUserAsync(userDto);
                    await uow.Commit();
                    return id;
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task<bool> Login(string username, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.AuthorizeUserAsync(username, password);
            }
        }
        
        public async Task<UserDTO> GetUserAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetUserAccordingToUsernameAsync(username);
            }
        }
        #endregion

        public async Task<IEnumerable<FreelancerDTO>> GetFreelancersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await freelancerService.ListAllAsync()).Items;
            }
        }

        public async Task<IEnumerable<CorporationDTO>> GetCorporationsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await corporationService.ListAllAsync()).Items;
            }
        }

        public async Task<IEnumerable<UnregisteredUserDTO>> GetUnregisteredsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await unregisteredService.ListAllAsync()).Items;
            }
        }

        public async Task<QueryResultDTO<FreelancerDTO, FreelancerFilterDTO>> GetFreelancersAsync(FreelancerFilterDTO filter)
        {
            using (UnitOfWorkProvider.Create())
            {                
                return await freelancerService.ListFreelancersAsync(filter);               
            }
        }        

        public async Task<QueryResultDTO<CorporationDTO, CorporationFilterDTO>> GetCorporationsAsync(CorporationFilterDTO filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await corporationService.ListCorporationsAsync(filter);
            }
        }

        public async Task<QueryResultDTO<UnregisteredUserDTO, UnregisteredUserFilterDTO>> GetUnregisteredsAsync(UnregisteredUserFilterDTO filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await unregisteredService.ListUnregisteredsAsync(filter);
            }
        }

        public double GetCorporationAverageRating(CorporationDTO corporation)
        {
            return corporation.Ratings.Average(rating => rating.rating.Score);
        }
    }
}
