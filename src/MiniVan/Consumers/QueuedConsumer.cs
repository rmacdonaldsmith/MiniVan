using System;
using System.Collections.Generic;
using System.Threading;

namespace MiniVan
{
	public class QueuedConsumer<T> : IConsume<T> where T : IMessage
	{
		private readonly IConsume<T> _inner;
		private readonly object _lock = new object();
		private readonly Queue<T> _messageQueue = new Queue<T> ();
		private readonly Thread _worker;

		public QueuedConsumer(IConsume<T> inner)
		{
			_inner = inner;
			(_worker = new Thread (Dequeue)).Start();
		}

		public void Handle (T msg)
		{
			lock (_lock) {
				_messageQueue.Enqueue (msg);
				Monitor.Pulse (_lock);
			}
		}

		private void Dequeue()
		{
			while (true) {
				T message;
				lock (_lock) {
					while (_messageQueue.Count == 0)
						Monitor.Wait(_lock);

					message = _messageQueue.Dequeue ();
					if (message == null)
						return; //this is the signal to exit - we can enqueue a NULL message and when we dequeue here we exit. means we can process existing messages in the q before we quit

					_inner.Handle (message);
				}
			}
		}
	}
}

