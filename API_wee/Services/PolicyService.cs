using API_wee.Data;
using API_wee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Policy> CreatePolicyAsync(int id_client, int id_typePolicy, int id_statusPolicy, string numPolicy, DateTime startDate, DateTime endDate, double amount )
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
        public async Task<Policy?> GetPolicyByIdAsync(int id)
        {
            return await _db.Policy.FindAsync(id);
        }

    }
}
