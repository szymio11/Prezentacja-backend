using System;
using System.Threading.Tasks;
using Api.NetCore.Domains;

namespace Logic.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<int> DeleteByCategoryIdAsync(Guid categoryId);
    }
}