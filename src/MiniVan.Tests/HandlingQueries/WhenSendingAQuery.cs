using NUnit.Framework;
using System;
using MiniVan.Tests.Helpers;
using MiniVan;

namespace MiniVan.Tests.HandlingQueries
{
	[TestFixture]
	public class WhenSendingAQuery : Given_a_bus_instance
	{
		private TestResponse _response;
		private int _anotherResponse;

		[TestFixtureSetUp]
		public void Initialize_Bus()
		{
			WithQueryHandler<TestRequest, TestResponse> (
				new FakeQueryHandler<TestRequest, TestResponse> (req =>
					new TestResponse{ CorrelationTag = req.CorrelationTag })
			);

			WithQueryHandler<TestRequest<int>, int> (
				new FakeQueryHandler<TestRequest<int>, int> (req => {
					_anotherResponse = -1;
					throw new AssertionException("The wrong query handler was invoked.");
				})
			);
		}

		[SetUp]
		public void When_a_query_is_sent()
		{
			var request = new TestRequest {CorrelationTag = "this is a query"};
			_response = Bus.Send (request);
		}

		[Test]
		public void Should_dispatch_to_the_expected_query_handler()
		{
			Assert.AreNotEqual (-1, _anotherResponse);
		}

		[Test]
		public void Should_receive_the_expected_response ()
		{
            Assert.IsNotNull(_response);
            Assert.AreEqual("this is a query", _response.CorrelationTag);
		}

		[Test]
		public void Response_should_be_of_the_expected_type()
		{
			Assert.IsNotNull (_response);
			Assert.IsInstanceOf<TestResponse>(_response);
		}
	}
}

