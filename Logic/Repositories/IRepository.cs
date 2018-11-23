using Api.NetCore.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);
        Task AddAsync(T entity);
        Task<int> SaveChangesAsync();
        void Remove(T t);
    }
}