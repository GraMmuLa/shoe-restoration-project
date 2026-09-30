using Microsoft.EntityFrameworkCore.Storage;
using ShoeRestorationProject.Context;

namespace ShoeRestorationProject.Helpers.Implementations
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(Action action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                action();
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        
        public T Execute(Func<T> action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                var result = action();
                _context.SaveChanges();
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task ExecuteAsync(Func<Task> func)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await func();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<T> ExecuteAsync(Func<Task<T>> func)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await func();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
        public async Task ExecuteAsync(Action func)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                func();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
        public async Task<T> ExecuteAsync(Func<T> func)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = func();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
