using NUnit.Framework;
using System;
using MiniVan.Tests.Helpers;

namespace MiniVan.Tests
{
	[TestFixture ()]
	public class WhenPublishingOneMessageToManySubscribers : Given_a_bus_instance
	{
		private IMessage _thePublishedMessage;

		[TestFixtureSetUp]
		public void Given_a_bus_with_many_subscribers()
		{
			WithConsumer (new FakeConsumer<TestMessages.TestMessage>(RecordRoutedMessages));
			WithConsumer (new FakeConsumer<TestMessages.TestMessage>(RecordRoutedMessages));
			WithConsumer (new FakeConsumer<TestMessages.TestMessage>(RecordRoutedMessages));
			WithConsumer (new FakeConsumer<TestMessages.TestMessage>(RecordRoutedMessages));

			_thePublishedMessage = new TestMessages.TestMessage ();
		}

		[SetUp]
		public void When_a_message_is_published()
		{
			Bus.Send (_thePublishedMessage);
		}

		[Test ()]
		public void Then_all_subscribers_receive_the_message ()
		{
			Assert.AreEqual (4, _routedMessages.Count);
		}
	}
}

