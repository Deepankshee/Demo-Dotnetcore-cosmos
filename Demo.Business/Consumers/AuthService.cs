using Demo.Business.Providers;
using Demo.Common.ViewModels;
using Demo.Common.DataModels;
using Demo.Repository.Consumers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Business.Common;
using Demo.Common.Constants;

namespace Demo.Business.Consumers
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository = null;
        private readonly IMapper mapper = null;
        public AuthService(IMapper mapper , IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        public async Task<AuthUserOutputModel> AuthenticateUser(AuthUserInputModel authUser)
        {
            try
            {
                var tokenDetails = new AuthUserOutputModel();
                var result = await userRepository.GetByCondition(item => item.LoginName == authUser.LoginName && item.Password == authUser.Password);
                var user = new User();
                foreach(var item in result)
                {
                    user = item;
                }
                
                if (user.LoginName!=null)
                {
                    tokenDetails = mapper.Map<AuthUserOutputModel>(user);
                    tokenDetails.Token = AuthModule.GenerateToken(tokenDetails);                   
                }
                return tokenDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
