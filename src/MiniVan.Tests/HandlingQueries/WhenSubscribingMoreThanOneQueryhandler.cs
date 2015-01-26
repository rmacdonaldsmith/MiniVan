using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MiniVan.Tests.Helpers;

namespace MiniVan.Tests.HandlingQueries
{
    [TestFixture]
    public class WhenSubscribingMoreThanOneQueryhandler : Given_a_bus_instance
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_throw()
        {
            WithQueryHandler(new FakeQueryHandler<TestRequest, TestResponse>(r => new TestResponse()));
            WithQueryHandler(new FakeQueryHandler<TestRequest, TestResponse>(r => new TestResponse()));
        }
    }
}
