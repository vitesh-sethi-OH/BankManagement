using BankManagement_ManagementAPI.Models;
using System.Linq.Expressions;

namespace BankManagement_ManagementAPI.Repository.IRepository
{
    public interface IBankRepository : IRepository<Bank>
    {
    
        Task<Bank> UpdateAsync(Bank entity);
 
    }
}
