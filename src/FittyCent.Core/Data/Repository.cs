using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FittyCent.Data {
    public sealed class Repository : IRepository {
        private readonly IDbContext _dbContext;

        public Repository(IDbContext dbContext) {
            _dbContext = dbContext;
        }

        private DbSet<T> DbSet<T>() where T : class {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> Query<T>() where T : class {
            return DbSet<T>();
        }

        public T Find<T>(params object[] keyValues) where T : class {
            return DbSet<T>().Find(keyValues);
        }

        public async Task<T> FindAsync<T>(params object[] keyValues) where T : class {
            return await DbSet<T>().FindAsync(keyValues);
        }

        public async Task<T> FindAsync<T>(CancellationToken cancellationToken, params object[] keyValues) where T : class {
            return await DbSet<T>().FindAsync(cancellationToken, keyValues);
        }

        public T Add<T>(T entity) where T : class {
            return DbSet<T>().Add(entity);
        }

        public T Attach<T>(T entity) where T : class {
            return DbSet<T>().Attach(entity);
        }

        public void Delete<T>(object id) where T : class {
            var entity = Find<T>(id);
            Delete(entity);
        }

        public void Delete<T>(T entity) where T : class {
            DbSet<T>().Remove(entity);
        }
    }
}