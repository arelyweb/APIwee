using API_wee.Models;
using System.Security.Claims;

namespace API_wee.Services
{
    public interface ITokenService
    {
        string CreateAccessToken(ApplicationUser user);
        RefreshToken CreateRefreshToken(string userId, int daysValid);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
