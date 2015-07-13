using MiniVan.Bus;

namespace MiniVan.Consumers
{
	public class NarrowingConsumer<TWideIn, TNarrowOut> : IConsume<TWideIn> 
		where TNarrowOut : TWideIn 
		where TWideIn : IMessage
	{
		private readonly IConsume<TNarrowOut> _innerConsumer;

		public NarrowingConsumer (IConsume<TNarrowOut> innerConsumer)
		{
			_innerConsumer = innerConsumer;
		}

		public void Handle(TWideIn message)
		{
			_innerConsumer.Handle ((TNarrowOut)message);
		}
	}
}

