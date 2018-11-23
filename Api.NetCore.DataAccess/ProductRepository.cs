using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.NetCore.Domains;
using Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Api.NetCore.DataAccess
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<int> DeleteByCategoryIdAsync(Guid categoryId)
        {
            return await DataContext.Products
                .Where(p => p.CategoryId == categoryId)
                .UpdateAsync(x => new Product() { IsActive = false });
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await DataContext.Products
                .Include(c=>c.Category)
                .Where(a => a.IsActive)
                .ToListAsync();
        }

        public override Task<Product> GetAsync(Guid id)
        {
            return DataContext.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}