using MiniVan.Bus;
using System.Threading.Tasks;
using MiniVan.Logging;

namespace MiniVan.Consumers
{
	public static class ConsumerExtensions
	{
		public static IConsume<TMessage> AsAsyncConsumer<TMessage>(this IConsume<TMessage> consumerToWrap) where TMessage : IMessage
		{
			return new AsyncConsumer<TMessage>(consumerToWrap);
		}

		public static IConsume<TMessage> AsQueuedConsumer<TMessage>(this IConsume<TMessage> consumerToWrap) where TMessage : IMessage
		{
			return new QueuedConsumer<TMessage> (consumerToWrap);
		}

		public static void LogAndSwallowExceptions(this Task task)
		{
			task.ContinueWith (t => {
				var e = t.Exception;
                if (e != null)
                    LogProvider.GetLogger(typeof(AsyncConsumer<>)).ErrorFormat("Unhandled exception in Task [{0}]: {1}", t.Id, e);
			}, TaskContinuationOptions.OnlyOnFaulted);
		}

		public static void ReThrowExceptions(this Task task)
		{
			task.ContinueWith (t => {
				var e = t.Exception;
                if (e != null)
				    throw e;
			}, TaskContinuationOptions.OnlyOnFaulted);
		}

		public static IConsume<TNarrowIn> WidenFrom<TNarrowIn, TWideOut>(this IConsume<TWideOut> handler)
			where TWideOut : IMessage
			where TNarrowIn : TWideOut
		{
			return new WideningConsumer<TNarrowIn, TWideOut>(handler);
		}

		public static IConsume<TWideIn> NarrowTo<TWideIn, TNarrowOut>(this IConsume<TNarrowOut> handler)
			where TWideIn : IMessage
			where TNarrowOut : TWideIn
		{
			return new NarrowingConsumer<TWideIn, TNarrowOut>(handler);
		}
	}
}

