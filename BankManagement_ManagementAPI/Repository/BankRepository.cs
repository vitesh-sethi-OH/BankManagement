using BankManagement_ManagementAPI.Data;
using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BankManagement_ManagementAPI.Repository
{
    public class BankRepository : Repository<Bank>, IBankRepository 
    {
        private readonly ApplicationDbContext _db;
        public BankRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Bank> UpdateAsync(Bank entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Banks.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
