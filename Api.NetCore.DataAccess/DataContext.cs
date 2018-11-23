using Api.NetCore.Domains;
using Microsoft.EntityFrameworkCore;

namespace Api.NetCore.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        :base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
