using System.Threading.Tasks;
using API_wee.Models;

namespace API_wee.Services
{
    public interface IUserService
    {
        Task<ApplicationUser?> AuthenticateAsync(string username, string password);
        Task<ApplicationUser> CreateUserAsync(string username, string lastname, string password, int roleId);
        Task<ApplicationUser?> GetByIdAsync(int id);
    }
}
