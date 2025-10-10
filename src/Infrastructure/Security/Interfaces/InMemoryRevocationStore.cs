using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Interfaces
{
    public class InMemoryRevocationStore : IRevocationStore
    {
        private static readonly ConcurrentDictionary<string, DateTime> _revoked = new();

        public Task<bool> IsRevokedAsync(string jti)
        {
            if (string.IsNullOrEmpty(jti)) return Task.FromResult(false);
            if (_revoked.TryGetValue(jti, out var expiry))
            {
                if (DateTime.UtcNow < expiry) return Task.FromResult(true);
                // expired -> remove
                _revoked.TryRemove(jti, out _);
            }
            return Task.FromResult(false);
        }

        public Task RevokeAsync(string jti, TimeSpan ttl)
        {
            if (string.IsNullOrEmpty(jti)) return Task.CompletedTask;
            var expiry = DateTime.UtcNow.Add(ttl);
            _revoked.AddOrUpdate(jti, expiry, (_, __) => expiry);
            return Task.CompletedTask;
        }
    }
}
