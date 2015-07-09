using System;
using MiniVan.Consumers;

namespace MiniVan
{
	public static class ExtensionMethods
	{
		public static IConsume<T> AsQueued<T>(this IConsume<T> innerConsumer) where T : IMessage
		{
			return new QueuedConsumer<T> (innerConsumer);
		}
	}
}

