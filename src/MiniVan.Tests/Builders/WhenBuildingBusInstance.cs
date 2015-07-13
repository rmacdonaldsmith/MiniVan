using System;
using System.Collections.Generic;
using FluentAssertions;
using MiniVan.Bus;
using MiniVan.Bus.Builder;
using MiniVan.Tests.HandlingQueries;
using MiniVan.Tests.Helpers;
using NUnit.Framework;

namespace MiniVan.Tests.Builders
{
    [TestFixture]
    public class WhenBuildingBusInstance
    {
        private readonly List<IMessage> _receivedMessages = new List<IMessage>();

        [TearDown]
        public void TearDown()
        {
            _receivedMessages.Clear();
        }

        [Test]
        public void ShouldReturnBusInstance()
        {
            Bus.Bus bus = new BusBuilder()
                .WithConsumer(new FakeConsumer<TestMessages.TestMessage>(msg => _receivedMessages.Add(msg)))
                .WithQuery(new FakeQueryHandler<TestRequest<TestResponse>, TestResponse>((request => new TestResponse())));

            bus.Should().NotBeNull();
            bus.Send(new TestMessages.TestMessage());
            _receivedMessages.Should().ContainSingle(msg => msg is TestMessages.TestMessage);
        }

        [Test]
        public void ShouldThrowIfNoConsumersSpecified()
        {
            var builder = new BusBuilder();

            Assert.Throws<ArgumentNullException>(() => builder.WithConsumer<TestMessages.TestMessage>(null));
        }
    }
}
