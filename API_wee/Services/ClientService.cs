using API_wee.Data;
using API_wee.Models;

namespace API_wee.Services
{
    public class ClientService: IClientService
    {
        private readonly AppDbContext _db;

        public ClientService(AppDbContext db){

            _db = db;
        }

        //Obtiene el cleinte por Id
        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _db.Client.FindAsync(id);
        }
    }
}
