namespace FittyCent.Core.Mediation {
    public interface ICommand : ICommand<UnitType> { }

    public interface ICommand<TResult> { }
}