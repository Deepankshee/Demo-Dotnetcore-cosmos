using Demo.Common.DataModels;
using Demo.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Providers
{
    public interface IProductService
    {
        Task<bool> Create(ProductViewModel user);

        Task<ProductListViewModel> GetProductsByUserId(string UserId);
    }
}
