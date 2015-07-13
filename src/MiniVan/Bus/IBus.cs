namespace MiniVan.Bus
{
    public interface IBus : ISendMessages, ISendQueries, ISubscribe, ISubscribeToQueries
    {
    }
}
