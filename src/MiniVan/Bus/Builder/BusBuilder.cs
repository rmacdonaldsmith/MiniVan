using MiniVan.Consumers;

namespace MiniVan.Bus.Builder
{
    public class BusBuilder
    {
        private readonly Bus _bus = new Bus();

        public BusBuilder WithConsumer<T>(IConsume<T> consumer) where T : class, IMessage
        {
            Ensure.NotNull(consumer, "consumer");
            _bus.Subscribe(consumer);
            return this;
        }

        public BusBuilder WithQuery<TReq, TResp>(IHandleQueries<TReq, TResp> queryHandler) where TReq : IRequest<TResp>
        {
            Ensure.NotNull(queryHandler, "queryHandler");
            _bus.Subscribe(queryHandler);
            return this;
        }

        public Bus Build()
        {
            return _bus;
        }
    }
}
