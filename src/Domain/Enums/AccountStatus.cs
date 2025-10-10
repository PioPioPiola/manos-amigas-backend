using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    internal class AccountStatus
    {
        public const char Unverified = 'U';
        public const char Active = 'A';
        public const char Inactive = 'I';
        public const char Suspended = 'S';
        public const char Reported = 'R';
    }
}
