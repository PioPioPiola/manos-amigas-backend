using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Interfaces
{
    public interface IRevocationStore
    {
        Task<bool> IsRevokedAsync(string jti);
        Task RevokeAsync(string jti, TimeSpan ttl);
    }
}
