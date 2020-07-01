using Demo.Common.Constants;
using Demo.Repository.Consumers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repository.Providers
{
    public class UserRepository : BaseRepository<Demo.Common.DataModels.User>, IUserRepository
    {
        public UserRepository(IConfiguration config) : base(config)
        {
        }
        public new async Task<bool> Update(string id, Demo.Common.DataModels.User user)
        {
            try
            {
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, id), user, new RequestOptions
                { PreTriggerInclude = new List<string> { Triggers.AddUpdateOnUpdate } });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
