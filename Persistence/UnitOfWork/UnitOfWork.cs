using Persistence.IRepositories;
using Persistence.Repositories;

namespace Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork.IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public IOrderRepository OrderRepository
        {
            get
            {
                return new OrderRepository(_context);
            }
            private set { }
        }
        public IOrderItemRepository OrderItemRepository
        {
            get
            {
                return new OrderItemRepository(_context);
            }
            private set { }
        }
        public ICustomerRepository CustomerRepository
        {
            get
            {
                return new CustomerRepository(_context);
            }
            private set { }
        }
        public IProductRepository ProductRepository
        {
            get
            {
                return new ProductRepository(_context);
            }
            private set { }
        }
    }
}
