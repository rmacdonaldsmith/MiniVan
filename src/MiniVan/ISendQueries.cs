namespace MiniVan
{
    public interface ISendQueries
    {
        TResponse Send<TResponse>(IRequest<TResponse> query);
    }
}