using System;
using NUnit.Framework;
using System.Collections.Generic;
using MiniVan.Consumers;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace MiniVan.Tests
{
	[TestFixture]
	public class WhenUsingAQueuedConsumer
	{
		private IConsume<TestMessages.TestMessage> _delegatingConsumer;
		private IConsume<TestMessages.TestMessage> _queuedConsumer;
		private readonly List<TestMessages.TestMessage> _received = new List<TestMessages.TestMessage>();

		[SetUp]
		public void SetupQueuedConsumer()
		{
			_delegatingConsumer = new DelegatingConsumer<TestMessages.TestMessage> ((msg) => {
				_received.Add(msg);
			});
			_queuedConsumer = new QueuedConsumer<TestMessages.TestMessage> (_delegatingConsumer);
		}

		[Test]
		public void ItShouldHandleMessages()
		{
			foreach (var item in Enumerable.Range(0,5))
				_queuedConsumer.Handle (new TestMessages.TestMessage{ Id = item.ToString () });

			Thread.Sleep (100);
			Assert.AreEqual (5, _received.Count);
		}

		[Test]
		public void ItShouldExitOnSystemStopMessage()
		{
            _queuedConsumer.Handle(new TestMessages.TestMessage());
            _queuedConsumer.Handle(new SystemMessage.Stop());
            _queuedConsumer.Handle(new TestMessages.TestMessage());

			Assert.AreEqual (1, _received.Count);
		}
	}
}

