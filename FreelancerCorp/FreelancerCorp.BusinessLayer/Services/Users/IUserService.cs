using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.Services.Users
{
    public interface IUserService
    {
        Task<int> RegisterFreelancerUserAsync(UserCreateFreelancerDTO user);
        Task<int> RegisterCorporationUserAsync(UserCreateCorporationDTO user);
        Task<bool> AuthorizeUserAsync(string username, string password);
        Task<UserDTO> GetUserAccordingToUsernameAsync(string username);
        Task<UserDTO> GetUserAccordingToIdAsync(int id);
    }
}
