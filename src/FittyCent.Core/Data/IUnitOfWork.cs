using System.Threading;
using System.Threading.Tasks;

namespace FittyCent.Data {
    public interface IUnitOfWork {
        void Save();
        Task<int> SaveAsync();
        Task<int> SaveAsync(CancellationToken cancellationToken);
        IRepository Repository { get; }
    }
}