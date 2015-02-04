namespace MiniVan.Consumers
{
    /// <summary>
    /// A decorator over the IConsume interface.
    /// This decorator allows the consumer to handle more general message types and downcast (widen) them so
    /// that the inner consumer can handle the message.
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    /// <typeparam name="TExpected"></typeparam>
	public class WideningConsumer<TNarrowInput, TWiderOut> : IConsume<TNarrowInput> 
		where TNarrowInput : TWiderOut
		where TWiderOut : IMessage
    {
		private readonly IConsume<TWiderOut> _innerConsumer;

		public WideningConsumer(IConsume<TWiderOut> innerConsumer)
        {
            _innerConsumer = innerConsumer;
        }

		public void Handle(TNarrowInput message)
        {
			_innerConsumer.Handle((TWiderOut)message);
        }
    }
}
