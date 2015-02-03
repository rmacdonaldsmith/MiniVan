using System;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using MiniVan.Tests.Helpers;

namespace MiniVan.Tests.Concurrency
{
	public class OneProducerThreeConsumers
	{
		private readonly object _lockObject = new object ();
		private int _numberOfMessagesToSend = 1000;
		private List<IMessage> _consumer1Received = new List<IMessage> ();
		private List<IMessage> _consumer2Received = new List<IMessage> ();
		private List<IMessage> _consumer3Received = new List<IMessage> ();
		private IBus _bus;
		private Random _rnd = new Random();

		[TestFixtureSetUp]
		public void WhenSendingMessages()
		{
			var c1 = new FakeConsumer<TestMessages.ADerivedTestMessage> (msg => Consumer1(msg));
			var c2 = new FakeConsumer<TestMessages.TestMessage> (msg => Consumer2(msg));
			var c3 = new FakeConsumer<IMessage> (msg => Consumer3(msg));

			_bus = new Bus ();
			_bus.Subscribe (c1);
			_bus.Subscribe (c2);
			_bus.Subscribe (c3);

			for (int i = 0; i < _numberOfMessagesToSend; i++) {
				_bus.Send (new TestMessages.ADerivedTestMessage{ Id = "P1-" + i });
			}
		}

		[Test]
		public void ShouldReceiveTheExpectedNumberOfMessages()
		{
			Assert.AreEqual (_numberOfMessagesToSend, _consumer1Received.Count);
			Assert.AreEqual (_numberOfMessagesToSend, _consumer2Received.Count);
			Assert.AreEqual (_numberOfMessagesToSend, _consumer3Received.Count);
		}

		private void RunAsync()
		{
			var t1 = new Thread (new ThreadStart (Producer1));
			t1.Start ();
			t1.Join ();
		}

		private void Producer1()
		{
			for (int i = 0; i < _numberOfMessagesToSend; i++) {
				_bus.Send (new TestMessages.ADerivedTestMessage{ Id = "P1-" + i });
			}
		}

		private void Consumer1(TestMessages.ADerivedTestMessage msg)
		{
			Thread.Sleep(_rnd.Next(5));
			_consumer1Received.Add (msg);
		}

		private void Consumer2(TestMessages.TestMessage msg)
		{
			Thread.Sleep(_rnd.Next(5));
			_consumer2Received.Add (msg);
		}

		private void Consumer3(IMessage msg)
		{
			Thread.Sleep(_rnd.Next(5));
			_consumer3Received.Add (msg);
		}
	}
}

