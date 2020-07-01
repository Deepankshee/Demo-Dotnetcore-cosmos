using Demo.Common.Constants;
using Demo.Repository.Providers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.Repository.Consumers
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected string collectionId = string.Empty;
        protected readonly string Endpoint = string.Empty;
        protected readonly string Key = string.Empty;
        protected readonly string DatabaseId = string.Empty;
        protected DocumentClient client;
        protected readonly IConfiguration config;
        public BaseRepository(IConfiguration config)
        {
            this.collectionId = GlobalConstants.CollectionType;          
            this.config = config;
            this.DatabaseId = config["AppSettings:DatabaseId"].ToString();
            this.Key = config["AppSettings:DatabaseKey"].ToString();
            this.Endpoint = config["AppSettings:DatabaseEndpoint"].ToString();
            client = new DocumentClient(new Uri(Endpoint), Key);
        }

        public async Task<bool> Create(T item)
        {
            try
            {
                await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), item);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> Get()
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }
            return results;
        }

        public async Task<bool> Update(string id, T item)
        {
            try
            {
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, id), item);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, id),
                new RequestOptions() { PartitionKey = new PartitionKey(GlobalConstants.BasePartitionKey) });
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                    new FeedOptions { MaxItemCount = -1,EnableCrossPartitionQuery = true })
                    .Where(predicate)
                    .AsDocumentQuery();

                List<T> results = new List<T>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateBySP(dynamic[] items)
        {
            try
            {
                Uri uri = UriFactory.CreateStoredProcedureUri(DatabaseId, collectionId, SPNames.SPCreateItem);
                RequestOptions options = new RequestOptions { PartitionKey = new PartitionKey(GlobalConstants.BasePartitionKey) };
                var result = await client.ExecuteStoredProcedureAsync<string>(uri, options, new[] { items });
                return Convert.ToInt32(result.Response) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
