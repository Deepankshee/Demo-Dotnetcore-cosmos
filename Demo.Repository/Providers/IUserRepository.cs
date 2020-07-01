using System;
using System.Collections.Generic;
using System.Text;
using Demo.Repository.Providers;
using Demo.Common.DataModels;

namespace Demo.Repository.Consumers
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
