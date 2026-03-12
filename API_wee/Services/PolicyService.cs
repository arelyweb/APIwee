using API_wee.Data;
using API_wee.Models;

namespace API_wee.Services
{
    public class PolicyService:IPolicyService
    {
        private readonly AppDbContext _db;

        public PolicyService(AppDbContext db)
        {
            _db = db;
        }
        // Crea policy
        public async Task<Policy> CreatePolicyAsync(short id_client, short id_typePolicy, short id_statusPolicy, string numPolicy, DateTime startDate, DateTime endDate, double amount )
        {
            var policy = new Policy
            {
                Id_client = id_client,
                Id_typePolicy = id_typePolicy,
                id_statusPolicy = id_statusPolicy,
                numPolicy = numPolicy,
                startDatePolicy = startDate, 
                endDatePolicy = endDate,
                amountPolicy = amount
            };

            _db.Policy.Add(policy);
            await _db.SaveChangesAsync();

            return policy;
        }

        //Obtiene la poliza por id
        public async Task<Policy?> GetByIdAsync(int id)
        {
            return await _db.Policy.FindAsync(id);
        }

    }
}
