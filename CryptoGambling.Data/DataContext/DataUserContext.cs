using CryptoGambling.Data.CashRegisters;
using CryptoGambling.Data.Funds;
using CryptoGambling.Data.Users;
using CryptoGambling.Data.WalletsData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.DataContext
{
    public class DataUserContext : DbContext
    {
        public DataUserContext(DbContextOptions<DataUserContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallets> Wallets { get; set; }
        public DbSet<Deposite> Deposites { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<CashRegister> CashRegister { get; set; }

    }
}
