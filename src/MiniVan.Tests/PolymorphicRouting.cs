using System;
using System.Collections.Generic;
using NUnit.Framework;
using MiniVan;
using MiniVan.Tests.Helpers;

namespace MiniVan
{
    [TestFixture]
	public class PolymorphicRouting : Given_a_bus_instance
    {
		private FakeConsumer<IMessage> _baseConsumer = new FakeConsumer<IMessage>();
		private FakeConsumer<TestMessages.TestMessage> _testMessageConsumer = new FakeConsumer<TestMessages.TestMessage>();
		private FakeConsumer<TestMessages.ADerivedTestMessage> _derivedMessageConsumer = new FakeConsumer<TestMessages.ADerivedTestMessage>();
		private FakeConsumer<TestMessages.NotDerivedTestMessage> _notADerivedMessageConsumer = new FakeConsumer<TestMessages.NotDerivedTestMessage>();

        [TestFixtureSetUp]
		public void When_a_TestMessage_is_published()
        {
			// this consumer should not receive the TestMessage that is published; there is no inheritance route to this message
			WithConsumer(_notADerivedMessageConsumer);
			WithConsumer(_baseConsumer);
			WithConsumer(_testMessageConsumer);
			WithConsumer(_derivedMessageConsumer); //this comsumer should not receive the message; ADerivedTestMessage is too specialized

			Bus.Send(new TestMessages.TestMessage());
        }

        [Test]
		public void then_the_base_subscriber_receives_the_message()
        {
			Assert.AreEqual(1, _baseConsumer.ReceivedMessages.Count);
        }

		[Test]
		public void then_the_TestMessage_subscriber_receives_the_message()
		{
			Assert.AreEqual(1, _testMessageConsumer.ReceivedMessages.Count);
		}

		[Test]
		public void then_the_NotADerivedMessage_subscriber_does_not_receive_the_message()
		{
			Assert.AreEqual(0, _notADerivedMessageConsumer.ReceivedMessages.Count);
		}

		[Test]
		public void then_the_ADerivedMessage_subscriber_does_not_receive_the_message()
		{
			Assert.AreEqual(0, _derivedMessageConsumer.ReceivedMessages.Count);
		}
    }
}
