using NUnit.Framework;
using System;
using System.Collections.Generic;
using MiniVan;
using MiniVan.Consumers;
using MiniVan.Tests.Helpers;

namespace MiniVan.Tests
{
	[TestFixture ()]
	public class WideningConsumerTests
	{
		[Test ()]
		public void CanHandleContravariantMessages ()
		{
			var handledMessages = new List<IMessage> ();
			IConsume<IMessage> consumer = new FakeConsumer<IMessage> (msg => handledMessages.Add (msg));
			var wideningConsumer = new WideningConsumer<TestMessages.ADerivedTestMessage, IMessage>(consumer);

			var message = new TestMessages.ADerivedTestMessage ();
			wideningConsumer.Handle (message);

			Assert.AreEqual (1, handledMessages.Count);
			Assert.IsInstanceOf<TestMessages.ADerivedTestMessage>(handledMessages[0]);
		}

		[Test()]
		[Ignore("This test exits for a reason! It demonstrates why you need a WideningConsumer. " +
			"This test is exactly the same as the test above, except there is no widening consumer, but this test will not compile.")]
		public void NonWideningFails()
		{
			var handledMessages = new List<IMessage> ();
			FakeConsumer<TestMessages.ADerivedTestMessage> consumer = 
				new FakeConsumer<TestMessages.ADerivedTestMessage> (msg => handledMessages.Add (msg));

			IMessage message = new TestMessages.ADerivedTestMessage ();
			//comment this line back in to see that the code will not compile
			//consumer.Handle (message);

			Assert.AreEqual (1, handledMessages.Count);
		}
	}
}

