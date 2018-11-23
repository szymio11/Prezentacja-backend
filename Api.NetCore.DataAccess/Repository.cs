using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.NetCore.Domains;
using Logic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.NetCore.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        public Repository(DataContext dataContext)
        {
            DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }
        protected readonly DataContext DataContext;

    
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DataContext.Set<T>()
                .Where(a => a.IsActive)
                .ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize)
        {
            return await DataContext.Set<T>()
                .Where(e => e.IsActive)
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
        public async Task AddAsync(T entity) => await DataContext.Set<T>().AddAsync(entity);

        public virtual async Task<T> GetAsync(Guid id)
        {
            return await DataContext.Set<T>().FindAsync(id);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await DataContext.SaveChangesAsync();
        }

        public virtual void Remove(T t)
        {
            t.IsActive = false;
        }
    }
}