using BankManagement_ManagementAPI.Data;
using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BankManagement_ManagementAPI.Repository
{
    public class BankLockerRepository : Repository<BankLocker>, IBankLockerRepository 
    {
        private readonly ApplicationDbContext _db;
        public BankLockerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<BankLocker> UpdateAsync(BankLocker entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.AccountNumber.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
