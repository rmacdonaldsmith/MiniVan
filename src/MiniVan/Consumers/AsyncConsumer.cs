using System;
using System.Threading.Tasks;
using MiniVan.Bus;

namespace MiniVan.Consumers
{
	public class AsyncConsumer<T> : IConsume<T> where T : IMessage
	{
		private readonly IConsume<T> _inner;

		public AsyncConsumer (IConsume<T> inner)
		{
			Ensure.NotNull (inner, nameof(inner));

			_inner = inner;
		}

		public void Handle (T msg)
		{
			var task = Task.Run(() => _inner.Handle(msg));
			task.LogAndSwallowExceptions();
			//do we need to catch any unhandled exceptions from the task?
			//yes, because if we do not then that exception could take down the entire process.
			//here, we use a continuation to check for an exception, then re-throw.
			//alternatively we can use TaskScheduler.UnobservedTaskException to catch unhandled exceptions from a Task
		}
	}
}

