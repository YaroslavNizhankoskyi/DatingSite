using System.Threading.Tasks;
using DatingSite.API.Models;

namespace DatingSite.API.Data
{
    public interface IAuthRepositry
    {
         Task<User> Register(User user, string password);

         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);
    }
}