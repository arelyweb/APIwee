using API_wee.Models;
using System.Collections.Concurrent;

namespace API_wee.Repositories
{
    // Implementación para demo. En producción, guarda en DB y almacena solo hash del token.

    public class InMemoryRefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _store = new();

        public Task SaveAsync(RefreshToken token)
        {
            _store[token.Token] = token;
            return Task.CompletedTask;
        }

        public Task<RefreshToken?> GetByTokenAsync(string token)
        {
            _store.TryGetValue(token, out var rt);
            return Task.FromResult(rt);
        }

        public Task RevokeAsync(string token, string? replacedBy = null)
        {
            if (_store.TryGetValue(token, out var rt))
            {
                rt.Revoked = true;
                rt.ReplacedBy = replacedBy;
            }
            return Task.CompletedTask;
        }

        public Task RemoveByUserAsync(string userId)
        {
            var keys = _store.Where(kv => kv.Value.Id_user == userId).Select(kv => kv.Key).ToList();
            foreach (var k in keys) _store.TryRemove(k, out _);
            return Task.CompletedTask;
        }
    }
}
