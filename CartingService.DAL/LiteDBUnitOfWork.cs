using CartingService.DAL.Interfaces;
using CartingService.DAL.LiteDb;
using LiteDB;
using Microsoft.Extensions.Options;

namespace CartingService.DAL
{
    public class LiteDBUnitOfWork : IUnitOfWork
    {
        private LiteDatabase database;
        private CartDAO cartDAO;

        public LiteDBUnitOfWork(IOptions<LiteDbOptions> options)
        {
            database = new LiteDatabase(options.Value.DatabaseLocation);
        }

        public ICartDAO Cart
        {
            get
            {
                if (cartDAO == null)
                {
                    cartDAO = new CartDAO(database);
                }
                return cartDAO;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    database.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
