using System;
using MiniVan.Consumers;

namespace MiniVan.Bus
{
    internal interface IDispatchQueries
	{
	    object Dispatch(IMessage query);
	}

    internal class QueryDispatcher<TReq, TRes> : IDispatchQueries where TReq : IRequest<TRes>
    {
        private readonly IHandleQueries<TReq, TRes> _consumer;

        public QueryDispatcher(IHandleQueries<TReq, TRes> consumer)
		{
			if (consumer == null)
				throw new ArgumentNullException("consumer");

			_consumer = consumer;
		}

        public object Dispatch(IMessage query)
		{
			if (query != null) {
				return _consumer.Handle ( (TReq) query);
			} else
				throw new NullReferenceException ("The query request was null.");
		}
	}
}

