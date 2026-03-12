using API_wee.Models;

namespace API_wee.Services
{
    public interface IPolicyService
    {
        Task<Policy> CreatePolicyAsync(short id_client, short id_typePolicy, short id_statusPolicy, string numPolicy, DateTime startDate, DateTime endDate, double amount);
        Task<Policy?> GetByIdAsync(int id);
    }
}
