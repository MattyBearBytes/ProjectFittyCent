namespace FittyCent.Core.Mediation {
    public interface ICommandHandler<in TCommand, TResult> {
        CommandResult<TResult> Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, UnitType> { }
}