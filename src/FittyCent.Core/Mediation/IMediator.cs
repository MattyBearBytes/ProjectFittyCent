namespace FittyCent.Core.Mediation {
    public interface IMediator {
        TResponse Request<TResponse>(IQuery<TResponse> query);
        CommandResult<TResponseData> Send<TResponseData>(ICommand<TResponseData> command);
    }
}