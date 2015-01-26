using System;
using System.Threading.Tasks;

namespace MiniVan.Consumers
{
	public class AsyncConsumer<T> : IConsume<T> where T : IMessage
	{
		private readonly IConsume<T> _inner;

		public AsyncConsumer (IConsume<T> inner)
		{
			Ensure.NotNull (inner, "inner");

			_inner = inner;
		}

		public void Handle (T msg)
		{
			Task.Run(() => _inner.Handle(msg));
		}
	}
}

