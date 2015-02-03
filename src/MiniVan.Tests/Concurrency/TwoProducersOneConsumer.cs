using System;
using NUnit.Framework;
using MiniVan.Tests.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace MiniVan.Tests.Concurrency
{
	[TestFixture]
	public class TwoProducersOneConsumer
	{
		private int _numberOfMessagesToSend = 10000;
		private List<TestMessages.ADerivedTestMessage> _receivedMessages = new List<TestMessages.ADerivedTestMessage> ();
		private IBus _bus;

		[TestFixtureSetUp]
		public void WhenSendingMessages()
		{
			var consumer = new FakeConsumer<TestMessages.ADerivedTestMessage> (msg => HandleReceived(msg));
			_bus = new Bus ();
			_bus.Subscribe (consumer);

			RunAsync ();
		}

		[Test]
		public void ShouldReceiveTheExpectedNumberOfMessages()
		{
			Assert.AreEqual (_numberOfMessagesToSend * 2, _receivedMessages.Count);
		}

		private void RunAsync()
		{
			var t1 = new Thread (new ThreadStart (Producer1));
			var t2 = new Thread (new ThreadStart (Producer2));

			t1.Start ();
			t2.Start ();

			t1.Join ();
			t2.Join ();
		}

		private void Producer1()
		{
			for (int i = 0; i < _numberOfMessagesToSend; i++) {
				_bus.Send (new TestMessages.ADerivedTestMessage{ Id = "P1-" + i });
			}
		}

		private void Producer2()
		{
				for (int i = 0; i < _numberOfMessagesToSend; i++) {
					_bus.Send (new TestMessages.ADerivedTestMessage{ Id = "P2-" + i });
				}
		}

		private void HandleReceived(TestMessages.ADerivedTestMessage msg)
		{
			lock (_receivedMessages) {
				_receivedMessages.Add (msg);
			}
		}
	}
}

