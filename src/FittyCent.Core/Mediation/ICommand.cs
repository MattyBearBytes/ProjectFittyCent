namespace FittyCent.Mediation {
    public interface ICommand : ICommand<UnitType> { }

    public interface ICommand<TResult> { }
}