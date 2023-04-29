using BankManagement_ManagementAPI.Models;
using System.Linq.Expressions;

namespace BankManagement_ManagementAPI.Repository.IRepository
{
    public interface IBankLockerRepository : IRepository<BankLocker>
    {
    
        Task<BankLocker> UpdateAsync(BankLocker entity);
 
    }
}
