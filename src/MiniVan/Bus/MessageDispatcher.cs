using MiniVan.Consumers;

namespace MiniVan.Bus
{
	internal interface IDispatchMessages
	{
		void Dispatch(IMessage message);
	}

	internal class MessageDispatcher<T> : IDispatchMessages where T : class, IMessage
	{
		private readonly IConsume<T> _consumer;

		public MessageDispatcher(IConsume<T> consumer)
		{
			Ensure.NotNull (consumer, "consumer");
			_consumer = consumer;
		}

		public void Dispatch(IMessage message)
		{
			var msg = message as T;
			if (msg != null)
			{
				_consumer.Handle(msg);
			}
		}
	}
}

