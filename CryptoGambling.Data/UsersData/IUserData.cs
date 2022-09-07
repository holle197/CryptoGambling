using CryptoGambling.Data.Users;
using CryptoGambling.Data.WalletsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.UserData
{
    public interface IUserData
    {
        // ServerSide for easy session handling
        User? GetUserByEmail(string email);
        User? GetUserByLoginId(string loginId);
        User? GetUserByRefLink(string reffLink);
        bool ReffLinkExist(string? reffLink);
        Task<bool> CreateNewUser(string email, string? referredBy);

        Wallets? GetWallets(string email);
        Dictionary<string, decimal> GetRefBalance(string email);
    }
}
