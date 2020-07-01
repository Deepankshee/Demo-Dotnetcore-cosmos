using AutoMapper;
using Demo.Business.Providers;
using Demo.Common.Constants;
using Demo.Common.Cryptography;
using Demo.Common.DataModels;
using Demo.Common.ViewModels;
using Demo.Repository.Consumers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Consumers
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository = null;
        private readonly IMapper mapper = null;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<bool> Create(UserViewModel user)
        {
            try
            {
                var userData = mapper.Map<User>(user);
                userData.Password = Cryptography.Encrypt(userData.Password);
                userData.BasePartitionKey = GlobalConstants.BasePartitionKey;
                userData.DocumentType = DocumentType.User;
                userData.Id = Guid.NewGuid();
                return await userRepository.Create(userData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(UserViewModel user)
        {
            try
            {
                var userData = mapper.Map<User>(user);
                userData.BasePartitionKey = GlobalConstants.BasePartitionKey;
                userData.DocumentType = DocumentType.User;
                return await userRepository.Update(Convert.ToString(userData.Id),userData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
