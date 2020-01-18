using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.QueryObjects.Common;
using FreelancerCorp.BusinessLayer.Services.Common;
using FreelancerCorp.DataAccessLayer.Entities;
using FreelancerCorp.Infrastructure;
using FreelancerCorp.Infrastructure.Query;

namespace FreelancerCorp.BusinessLayer.Services.Users
{
    public class UserService : ServiceBase, IUserService
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        private IRepository<Freelancer> freelancerRepository;
        private IRepository<Corporation> corporationRepository;
        private readonly QueryObjectBase<UserDTO, User, UserFilterDTO, IQuery<User>> userQueryObject;

        public UserService(IMapper mapper, IRepository<Freelancer> freelancerRep, IRepository<Corporation> corporationRep,
            QueryObjectBase<UserDTO, User, UserFilterDTO, IQuery<User>> userQueryObject)
            : base(mapper)
        {
            freelancerRepository = freelancerRep;
            corporationRepository = corporationRep;
            this.userQueryObject = userQueryObject;
        }
        public async Task<bool> AuthorizeUserAsync(string username, string password)
        {
            var userResult = await userQueryObject.ExecuteQuery(new UserFilterDTO { UserName = username });
            var user = userResult.Items.SingleOrDefault();

            var succ = user != null && VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password);            
            return succ;
        }

        public async Task<UserDTO> GetUserAccordingToUsernameAsync(string username)
        {
            var queryResult = await userQueryObject.ExecuteQuery(new UserFilterDTO() { UserName = username });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<UserDTO> GetUserAccordingToIdAsync(int id)
        {
            var queryResult = await userQueryObject.ExecuteQuery(new UserFilterDTO() { UserId = id });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<int> RegisterFreelancerUserAsync(UserCreateFreelancerDTO userDto)
        {
            var freelancer = Mapper.Map<Freelancer>(userDto);
            
            if (await GetIfUserExistsAsync(freelancer.UserName))
            {
                throw new ArgumentException("User already exists");
            }
            freelancer.UserRole = "Freelancer";

            var password = CreateHash(userDto.Password);
            freelancer.PasswordHash = password.Item1;
            freelancer.PasswordSalt = password.Item2;

            freelancerRepository.Create(freelancer);

            return freelancer.Id;            
        }

        public async Task<int> RegisterCorporationUserAsync(UserCreateCorporationDTO userDto)
        {
            var corporation = Mapper.Map<Corporation>(userDto);

            if (await GetIfUserExistsAsync(corporation.Name))
            {
                throw new ArgumentException();
            }
            corporation.UserRole = "Corporation";
            corporation.UserName = corporation.Name;

            var password = CreateHash(userDto.Password);
            corporation.PasswordHash = password.Item1;
            corporation.PasswordSalt = password.Item2;

            corporationRepository.Create(corporation);

            return corporation.Id;

        }

        private async Task<bool> GetIfUserExistsAsync(string username)
        {
            var queryResult = await userQueryObject.ExecuteQuery(new UserFilterDTO { UserName = username });
            return (queryResult.Items.Count() == 1);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }        
    }
}
