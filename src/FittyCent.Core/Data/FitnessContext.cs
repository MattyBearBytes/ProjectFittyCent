using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace FittyCent.Data {
    public class FitnessContext : DbContext, IDbContext {
        public FitnessContext()
            : this("projectfittycent") { }

        public FitnessContext(string nameOrConnectionString)
            : base(nameOrConnectionString) { }

        protected sealed override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType()));
            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseScript() {
            return ( (IObjectContextAdapter) this ).ObjectContext.CreateDatabaseScript();
        }
    }
}