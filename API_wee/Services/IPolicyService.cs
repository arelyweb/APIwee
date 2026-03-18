using API_wee.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_wee.Services
{
    public interface IPolicyService
    {
        Task<Policy> CreatePolicyAsync(int id_client, int id_typePolicy, int id_statusPolicy, string numPolicy, DateTime startDate, DateTime endDate, double amount);
        Task<Policy?> GetPolicyByIdAsync(int id);
    }
}
