using API_wee.Data;
using API_wee.Models;
using Microsoft.EntityFrameworkCore;

namespace API_wee.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _db;

        public ClientService(AppDbContext db) {

            _db = db;
        }

        //Obtiene el cleinte por Id
        public async Task<Client?> GetClientByIdAsync(int id)
        {
            var clients = await _db.Client.FindAsync(id);

            return clients;
        }

        public async Task<IEnumerable<Client?>> GetClientAsync()
        {
            //mostrar todos los clientes

            var clients = await _db.Client.ToListAsync();
            return clients;

        }


        } 
}
