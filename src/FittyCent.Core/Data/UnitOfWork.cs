using System.Threading;
using System.Threading.Tasks;

namespace FittyCent.Data {
    public sealed class UnitOfWork : IUnitOfWork {
        private readonly IDbContext _dbContext;
        private readonly IRepository _repository;


        public UnitOfWork(IDbContext dbContext, IRepository repository) {
            _dbContext = dbContext;
            _repository = repository;
        }

        public IRepository Repository { get { return _repository; } }


        public void Save() {
            _dbContext.SaveChanges();
        }

        public Task<int> SaveAsync() {
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken) {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}