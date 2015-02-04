using System;

namespace MiniVan.Consumers
{
	public class DelegatingConsumer<T> : IConsume<T> where T : IMessage
	{
		private readonly Action<T> _delegate;

		public DelegatingConsumer (Action<T> delegatingAction)
		{
			Ensure.NotNull (delegatingAction, "delegatingAction");
			_delegate = delegatingAction;
		}

		public void Handle (T msg)
		{
			_delegate (msg);
		}
	}
}

