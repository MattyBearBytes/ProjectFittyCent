using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FittyCent.Data {
    public interface IRepository {
        IQueryable<T> Query<T>() where T : class;
        T Find<T>(params object[] keyValues) where T : class;
        Task<T> FindAsync<T>(params object[] keyValues) where T : class;
        Task<T> FindAsync<T>(CancellationToken cancellationToken, params object[] keyValues) where T : class;
        T Add<T>(T entity) where T : class;
        void Delete<T>(object id) where T : class;
        void Delete<T>(T entity) where T : class;
        T Attach<T>(T entity) where T : class;
    }
}