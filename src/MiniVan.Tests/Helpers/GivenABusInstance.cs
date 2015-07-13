using System;
using System.Collections.Generic;
using MiniVan.Bus;
using MiniVan.Consumers;
using NUnit.Framework;

namespace MiniVan.Tests.Helpers
{
	public abstract class Given_a_bus_instance
	{
		protected readonly List<IMessage> _routedMessages = new List<IMessage>();
		private Action<IMessage> _handleMessageDelegate;
		private Bus.Bus _bus = new Bus.Bus();

		[TestFixtureSetUp]
		public void Setup()
		{
			_handleMessageDelegate = new Action<IMessage> (m => _routedMessages.Add (m));
		}

		public IBus Bus 
		{
			get { return _bus; }
		}

		public Action<IMessage> RecordRoutedMessages
		{
			get { return _handleMessageDelegate; }
		}

		public List<IMessage> RoutedMessages {
			get { return _routedMessages; }
		}

		public void WithMessageHandler (Action<IMessage> handler)
		{
			_handleMessageDelegate = handler;
		}

		public void WithConsumer<T>(IConsume<T> consumer) where T : class, IMessage
		{
			Bus.Subscribe (consumer);
		}

		public void WithQueryHandler<TRequest, TResponse>(IHandleQueries<TRequest, TResponse> queryHandler)  where TRequest : IRequest<TResponse> 
		{
			Bus.Subscribe (queryHandler);
		}
	}
}

