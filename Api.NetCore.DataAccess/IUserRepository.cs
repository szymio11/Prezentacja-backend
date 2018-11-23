using System.Threading.Tasks;
using Api.NetCore.Domains;

namespace Api.NetCore.DataAccess
{
    public interface IUserRepository
    {
        Task<User> Get(string username, string password);
        Task<User> GetByName(string username);
    }
}