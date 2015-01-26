namespace MiniVan.Consumers
{
    /// <summary>
    /// A decorator over the IConsume interface.
    /// This decorator allows the consumer to handle more general message types and downcast (widen) them so
    /// that the inner consumer can handle the message.
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    /// <typeparam name="TExpected"></typeparam>
	public class WideningConsumer<TInput, TOutput> : IConsume<TOutput> 
		where TInput : TOutput
		where TOutput : IMessage
    {
		private readonly IConsume<TInput> _innerConsumer;

		public WideningConsumer(IConsume<TInput> innerConsumer)
        {
            _innerConsumer = innerConsumer;
        }

		public void Handle(TOutput message)
        {
			//dangerous - essentially this is going to "swallow" the case when the cast is not valid
			//if (message is TOutput)
			_innerConsumer.Handle((TInput)message);
        }
    }
}
