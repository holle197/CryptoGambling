using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.Users
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public bool IsVerified { get; set; }
        public string? UserLoginId { get; set; }

        public string? ReferredBy { get; set; }
        public string? ReferralLink { get; set; }
    }
}
