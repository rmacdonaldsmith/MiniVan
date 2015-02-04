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
			//do we need to catch any unhandled exceptions from the task?
			//yes, because if we do not then that exception could take down the entire process.
		}
	}
}

