using System.Linq;
using System.Threading.Tasks;
using Api.NetCore.Domains;
using Microsoft.EntityFrameworkCore;

namespace Api.NetCore.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Get(string username,string password)
        {
            var user = await _context.Users.Where(a => a.Username == username && a.Password == password)
                .SingleAsync();
            return user;
        }
        public async Task<User> GetByName(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Username == username);
            return user;
        }
    }
}
