using System;
using MiniVan.Consumers;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MiniVan.Consumers
{
	public static class ConsumerExtensions
	{
		public static IConsume<TMessage> AsAsyncConsumer<TMessage>(this IConsume<TMessage> consumerToWrap) where TMessage : IMessage
		{
			return new AsyncConsumer<TMessage>(consumerToWrap);
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

