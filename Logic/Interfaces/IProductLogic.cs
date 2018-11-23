using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.NetCore.Domains;

namespace Logic.Interfaces
{
    public interface IProductLogic : ILogic
    {
        Task<Result<IEnumerable<Product>>> GetAllAsync();
        Task<Result<Product>> GetByIdAsync(Guid id);
        Task<Result<Product>> CreateAsync(Product product);
        Task<Result<Product>> RemoveAsync(Product product);
        Task<Result<Product>> UpdateAsync(Product product);
    }
}