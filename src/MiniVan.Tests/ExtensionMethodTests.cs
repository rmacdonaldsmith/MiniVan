using System;
using NUnit.Framework;
using MiniVan.Consumers;

namespace MiniVan.Tests
{
	[TestFixture]
	public class ExtensionMethodTests
	{
		[Test]
		public void ShouldReturnAQueuedConsumer()
		{
			var innerConsumer = new Helpers.FakeConsumer<TestMessages.TestMessage> ();
			var queuedConsumer = innerConsumer.AsQueuedConsumer ();

			Assert.IsInstanceOf (typeof(QueuedConsumer<TestMessages.TestMessage>), queuedConsumer);
		}
	}
}

