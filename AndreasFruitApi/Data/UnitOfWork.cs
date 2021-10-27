using System.Threading.Tasks;
using AndreasFruit_api.Interfaces;
using AndreasFruit_api.Repositories;

namespace AndreasFruit_api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FruitContext _context;
        public UnitOfWork(FruitContext context)
        {
            _context = context;
        }

        public IFruitRepository FruitRepository => new FruitRepository(_context);

        public ICategoryRepository CategoryRepository => new CategoryRepository(_context);

        public FruitContext Context => _context;

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanged()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}