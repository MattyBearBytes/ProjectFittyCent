namespace FittyCent.Mediation {
    public interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery<TResponse> {
        TResponse Handle(TQuery query);
    }
}
