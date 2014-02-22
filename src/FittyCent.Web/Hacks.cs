using FittyCent.Data;

namespace FittyCent.Web {
    public class Hacks {
        public static IUnitOfWork CreateUnitOfWork() {
            var context = new FitnessContext();
            var repository = new Repository(context);
            return new UnitOfWork(context, repository);
        }
    }
}