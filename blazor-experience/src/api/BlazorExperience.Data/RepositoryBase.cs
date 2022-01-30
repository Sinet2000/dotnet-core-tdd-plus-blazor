using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlazorExperience.Core.Models.Bases;
using Microsoft.EntityFrameworkCore;

namespace BlazorExperience.Data
{
    public abstract class RepositoryBase<T> where T : ModelBase
    {
        protected IDataContext DataContext;
        protected readonly DbSet<T> DbSet;

        protected RepositoryBase(IDataContext dataContext)
        {
            DataContext = dataContext;
            DbSet = dataContext.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await DataContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await (predicate != null ? DbSet.Where(predicate) : DbSet).CountAsync();
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await (predicate != null ? DbSet.Where(predicate) : DbSet).FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await (predicate != null ? DbSet.Where(predicate) : DbSet).ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            DbSet.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;

            await DataContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Attach(entity);
                DataContext.Entry(entity).State = EntityState.Modified;
            }

            await DataContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);

            await DataContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(List<T> entities)
        {
            DbSet.RemoveRange(entities);

            await DataContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            IEnumerable<T> objects = DbSet.Where<T>(expression).AsEnumerable();

            foreach (T obj in objects)
                DbSet.Remove(obj);

            await DataContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet;
        }
    }
}
