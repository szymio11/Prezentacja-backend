using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.NetCore.Domains;

namespace Logic.Interfaces
{
    public interface ICategoryLogic : ILogic
    {
        Task<Result<Category>> CreateAsync(Category category);
        Task<Result<IEnumerable<Category>>> GetAllAsync();
        Task<Result<Category>> UpdateAsync(Category category);
        Task<Result<Category>> GetAsync(Guid id);
        Task<Result> RemoveAsync(Category category);

    }
}