using API_wee.Models;

namespace API_wee.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task SaveAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeAsync(string token, string? replacedBy = null);
        Task RemoveByUserAsync(string userId); // útil al logout
    }
}
