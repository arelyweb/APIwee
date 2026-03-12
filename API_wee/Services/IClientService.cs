using API_wee.Models;

namespace API_wee.Services
{
    public interface IClientService
    {
          Task<Client?> GetByIdAsync(int id);
    }
}
