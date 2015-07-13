using MiniVan.Bus;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using MiniVan.Tests.Helpers;

namespace MiniVan.Tests
{
	[TestFixture ()]
	public class WhenPublishingManyMessagesToOneSubscriber : Given_a_bus_instance
	{
		private readonly List<IMessage> _messagesToPublish = new List<IMessage>();

		[TestFixtureSetUp]
		public void Given_a_bus_with_a_single_subscriber()
		{
			WithConsumer (new FakeConsumer<TestMessages.TestMessage>(RecordRoutedMessages));

			_messagesToPublish.Add (new TestMessages.ADerivedTestMessage());
			_messagesToPublish.Add (new TestMessages.SiblingOfADerivedTestMessage());
			_messagesToPublish.Add (new TestMessages.ADerivedTestMessage());
		}

		[SetUp]
		public void When_messages_are_published()
		{
			_messagesToPublish.ForEach (msg => Bus.Send (msg));
		}

		[Test ()]
		public void Then_the_subscriber_receives_all_messages ()
		{
			CollectionAssert.AreEquivalent (_messagesToPublish, _routedMessages);
		}
	}
}

