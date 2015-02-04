using System;
using NUnit.Framework;
using MiniVan.Tests.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace MiniVan.Tests.Concurrency
{
	[TestFixture]
	public class ThreeProducersOneConsumer
	{
		private int _numberOfMessagesToSend = 100;
		private List<TestMessages.ADerivedTestMessage> _receivedMessages = new List<TestMessages.ADerivedTestMessage> ();
		private IBus _bus;
		private Random _rnd = new Random();

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
			Assert.AreEqual (_numberOfMessagesToSend * 3, _receivedMessages.Count);
		}

		private void RunAsync()
		{
			var t1 = new Thread (new ThreadStart (Producer1));
			var t2 = new Thread (new ThreadStart (Producer2));
			var t3 = new Thread (new ThreadStart (Producer3));

			t1.Start ();
			t2.Start ();
			t3.Start ();

			t1.Join ();
			t2.Join ();
			t3.Join ();
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

		private void Producer3()
		{
			for (int i = 0; i < _numberOfMessagesToSend; i++) {
				_bus.Send (new TestMessages.ADerivedTestMessage{ Id = "P3-" + i });
			}
		}

		private void HandleReceived(TestMessages.ADerivedTestMessage msg)
		{
			lock (_receivedMessages) {
				Thread.Sleep(_rnd.Next(5));
				_receivedMessages.Add (msg);
			}
		}
	}
}

