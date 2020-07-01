using Demo.Common.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Providers
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<ProductListModel>> GetProductsByUserId(string UserId);
    }
}
