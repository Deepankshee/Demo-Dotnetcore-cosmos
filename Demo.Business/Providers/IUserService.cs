using Demo.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Providers
{
    public interface IUserService
    {
        Task<bool> Create(UserViewModel user);
        Task<bool> Update(UserViewModel user);
    }
}
