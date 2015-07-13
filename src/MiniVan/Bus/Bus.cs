using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MiniVan.Consumers;
using MiniVan.Topics;

namespace MiniVan.Bus
{
	//playing arround with implementations
	//this bus uses a List of a non-generic wrapper type that delegates messages to their
	//wrapped IConsumer<T> instance - avoids the generics poking through in to the client facing Bus class
	public class Bus : IBus
    {
		private readonly ITopicFactory<Type> _topicFactory = new MessageTypeTopicFactory();
		private readonly ConcurrentDictionary<string, List<IDispatchMessages>> _subscribers = 
			new ConcurrentDictionary<string, List<IDispatchMessages>>();
		private readonly ConcurrentDictionary<string, IDispatchQueries> _queryHandlers =
			new ConcurrentDictionary<string, IDispatchQueries> ();

        public void Send(IMessage message)
        {
            var topics = _topicFactory.GetTopicsFor(message.GetType());
			foreach (var type in topics)
            {
                SendToTopic(type, message);
            }
        }

	    private void SendToTopic(string topic, IMessage message)
        {
			List<IDispatchMessages> handlers;
			if (_subscribers.TryGetValue (topic, out handlers)) {
				foreach (var handler in handlers) {
					handler.Dispatch (message);
				}
			}
        }

		public TResponse Send<TResponse>(IRequest<TResponse> query)
		{
			IDispatchQueries handler;
			if(_queryHandlers.TryGetValue(query.GetType().FullName, out handler)) {
				return (TResponse) handler.Dispatch(query);
			}

			return default(TResponse);
		}

		public void Subscribe<T>(IConsume<T> consumer) where T : class, IMessage
        {
			_subscribers.AddOrUpdate (
				typeof(T).FullName,
				s => {
					var list = new List<IDispatchMessages> ();
					list.Add(new MessageDispatcher<T> (consumer));
					return list;
				},
				(_, list) => {
					list.Add (new MessageDispatcher<T> (consumer));
					return list;
				});
        }

		public void Subscribe<TRequest, TResponse> (IHandleQueries<TRequest, TResponse> queryhandler) where TRequest : IRequest<TResponse>
		{
			if(!_queryHandlers.TryAdd (typeof(TRequest).FullName, new QueryDispatcher<TRequest, TResponse> (queryhandler)))
				throw new InvalidOperationException(
					string.Format("Cannot subscribe because a query handler is already registered for request of type [{0}]", typeof(TRequest).FullName));
		}

        public void ClearSubscribers()
        {
            _subscribers.Clear();
        }
    }
}
