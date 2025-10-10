using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManoaAmigas.Domain.Utilities
{
    public class AuthHelpers
    {
        public AuthHelpers()
        {
        }
        public static string GenerateNumericId()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

            string random = new Random().Next(100, 999).ToString();

            return timestamp + random;
        }
    }
}
