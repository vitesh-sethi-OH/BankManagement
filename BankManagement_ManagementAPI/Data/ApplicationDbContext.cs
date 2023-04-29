using BankManagement_ManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagement_ManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {

        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankLocker> AccountNumber { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>().HasData(
                new Bank()
                {
                    AccNo = 1,
                    AccName = "vitesh",
                    AccType = "savings",
                    AadharCard = 64848,
                    PanCard = "wert326y8",
                    Address = "reikhjtgoiew",
                    CreatedDate= DateTime.Now
                }
                );
        }
    }
}
