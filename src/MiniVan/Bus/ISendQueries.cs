namespace MiniVan.Bus
{
    public interface ISendQueries
    {
        TResponse Send<TResponse>(IRequest<TResponse> query);
    }
}