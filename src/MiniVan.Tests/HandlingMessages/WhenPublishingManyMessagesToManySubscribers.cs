using MiniVan.Bus;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using MiniVan.Tests.Helpers;

namespace MiniVan.Tests
{
	[TestFixture ()]
	public class WhenPublishingManyMessagesToManySubscribers : Given_a_bus_instance
	{
		private readonly List<IMessage> _messagesToPublish = new List<IMessage>();

		[TestFixtureSetUp]
		public void Given_a_bus()
		{	

			WithConsumer (new FakeConsumer<TestMessages.ADerivedTestMessage> (RecordRoutedMessages));
			WithConsumer (new FakeConsumer<TestMessages.NotDerivedTestMessage>(RecordRoutedMessages));
			WithConsumer (new FakeConsumer<TestMessages.SiblingOfADerivedTestMessage> (RecordRoutedMessages));

			_messagesToPublish.Add (new TestMessages.ADerivedTestMessage());
			_messagesToPublish.Add (new TestMessages.NotDerivedTestMessage());
			_messagesToPublish.Add (new TestMessages.SiblingOfADerivedTestMessage());
			_messagesToPublish.Add (new TestMessages.ADerivedTestMessage());
		}

		[SetUp]
		public void When_messages_are_published()
		{
			_messagesToPublish.ForEach (msg => Bus.Send (msg));
		}

		[Test]
		public void Then_all_messages_are_received()
		{
			CollectionAssert.AreEquivalent (_messagesToPublish, _routedMessages);
		}
	}
}

