using API_wee.Models;

namespace API_wee.Services
{
    public interface IClientService
    {
          Task<Client?> GetClientByIdAsync(int id);
        Task<IEnumerable<Client?>> GetClientAsync();
    }
}
