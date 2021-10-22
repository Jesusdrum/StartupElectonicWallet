using Microsoft.EntityFrameworkCore;
using StartupElectonicWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupElectonicWallet.DataAccess
{
    public class ElectronicWalletContext : DbContext
    {
        public ElectronicWalletContext(DbContextOptions<ElectronicWalletContext> options)
        : base(options){

        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

    }
}
