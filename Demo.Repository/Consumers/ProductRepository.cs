using Demo.Common.Constants;
using Demo.Common.DataModels;
using Demo.Repository.Providers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repository.Consumers
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<List<ProductListModel>> GetProductsByUserId(string userId)
        {
            try
            {
                Uri uri = UriFactory.CreateStoredProcedureUri(DatabaseId, collectionId, SPNames.SPGetProductByUsrId);
                RequestOptions options = new RequestOptions { PartitionKey = new PartitionKey(GlobalConstants.BasePartitionKey) };
                return await client.ExecuteStoredProcedureAsync<List<ProductListModel>>(uri, options, new[] { userId });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
