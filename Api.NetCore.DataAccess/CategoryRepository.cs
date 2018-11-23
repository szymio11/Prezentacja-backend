using System;
using Api.NetCore.Domains;
using Logic.Repositories;

namespace Api.NetCore.DataAccess
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}