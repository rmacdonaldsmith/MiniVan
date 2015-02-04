using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MiniVan.Tests
{
	public abstract class ConsumerFixture<TMessage> where TMessage : IMessage
	{
		protected abstract IConsume<TMessage> GivenConsumer();
		protected abstract IEnumerable<TMessage> When();
		protected IEnumerable<TMessage> _sent;
		protected IConsume<TMessage> _consumer;
		protected Exception _exception;

		[SetUp]
		public virtual void SetUp()
		{
			_consumer = GivenConsumer ();
			try {
				_sent = When();
				if(_sent != null)
					foreach (var message in _sent) {
						_consumer.Handle(message);
					}

			} catch (Exception ex) {
				_exception = ex;
			}
		}
	}
}

