using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace FittyCent.Data {
    public interface IDbContext : IDisposable {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}