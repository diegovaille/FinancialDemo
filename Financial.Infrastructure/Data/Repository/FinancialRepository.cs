using FinancialDemo.Core.Entities;
using FinancialDemo.Core.Interfaces;
using FinancialDemo.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialDemo.Infrastructure.Data.Repository
{
    public class FinancialRepository : IRepository
    {
        private readonly FinancialDbContext _dbContext;

        public FinancialRepository(FinancialDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _dbContext.Set<Account>().Include(a => a.Transactions).SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _dbContext.Set<Account>().Include(a => a.Transactions).ToListAsync();
        }

        public async Task<T> GetById<T>(int id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> List<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
