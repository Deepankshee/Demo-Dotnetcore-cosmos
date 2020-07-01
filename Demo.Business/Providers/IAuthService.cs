using Demo.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Providers
{
    public interface IAuthService
    {
        Task<AuthUserOutputModel> AuthenticateUser(AuthUserInputModel authUser);
    }
}
