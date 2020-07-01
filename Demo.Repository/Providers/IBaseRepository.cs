using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Providers
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> Create(T item);
        Task Delete(string id);       
        Task<IEnumerable<T>> Get();
        Task<bool> Update(string id, T item);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate);
        Task<bool> CreateBySP(dynamic[] items);
    }
}
