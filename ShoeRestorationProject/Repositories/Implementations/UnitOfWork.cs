using System.Transactions;
using ShoeRestorationProject.Context;

namespace ShoeRestorationProject.Helpers.Implementations
{
    public class UnitOfWork(AppDbContext context,
        ILogger<UnitOfWork> logger) : IUnitOfWork
    {
        public void Execute(Action action)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                action();
                context.SaveChanges();
                transaction.Commit();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Transaction failed");
                
                transaction.Rollback();
                throw new TransactionException("Transaction failed");
            }
        }
        
        public T Execute<T>(Func<T> action)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var result = action();
                context.SaveChanges();
                transaction.Commit();
                return result;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Transaction failed");
                transaction.Rollback();
                throw new TransactionException("Transaction failed");
            }
        }

        public async Task ExecuteAsync(Func<Task> func)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await func();
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Transaction failed");
                await transaction.RollbackAsync();
                throw new TransactionException("Transaction failed");
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> func)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var result = await func();
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Transaction failed");
                await transaction.RollbackAsync();
                throw new TransactionException("Transaction failed");
            }
        }
        
        public async Task ExecuteAsync(Action func)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                func();
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Transaction failed");
                
                await transaction.RollbackAsync();
                throw new TransactionException("Transaction failed");
            }
        }
        
        public async Task<T> ExecuteAsync<T>(Func<T> func)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var result = func();
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Transaction failed");
                
                await transaction.RollbackAsync();
                throw new TransactionException("Transaction failed");
            }
        }
    }
}
