using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExperience.Data
{
    public interface IAsyncRepository<T> where T : class
    {
        Task UpdateAsync(T entity);
        Task UpdateAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(List<T> entities);
        Task DeleteAsync(Expression<Func<T, bool>> where);
        Task<T> GetByIdAsync(long id);
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> CreateAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();

    }
}
