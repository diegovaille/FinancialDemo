using FinancialDemo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialDemo.Core.Interfaces
{

    public interface IRepository
    {
        Task<List<Account>> GetAccounts();
        Task<Account> GetAccountById(int id);

        Task<T> GetById<T>(int id) where T : BaseEntity;
        Task<List<T>> List<T>() where T : BaseEntity;
        Task<T> Add<T>(T entity) where T : BaseEntity;
        Task Update<T>(T entity) where T : BaseEntity;
        Task Delete<T>(T entity) where T : BaseEntity;
    }
}