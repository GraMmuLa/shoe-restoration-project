using Microsoft.EntityFrameworkCore.Storage;
using Shoe_restoration_project.Context;

namespace Shoe_restoration_project.Helpers.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(Action action)
        {
            using IDbContextTransaction transaction = _context.Database.BeginTransaction();
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
    }
}
