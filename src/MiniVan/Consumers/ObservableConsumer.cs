using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Reactive.Subjects;

namespace MiniVan.Consumers
{
	public class DelegatingConsumer<T> : IConsume<T> where T : IMessage
	{
		private readonly Action<T> _delegate;

		public DelegatingConsumer (Action<T> delegatingAction)
		{
			Ensure.NotNull (delegatingAction, "delegatingAction");
			_delegate = delegatingAction;
		}

		public void Handle (T msg)
		{
			_delegate (msg);
		}
	}
		
	public class ObservableConsumerWrapper<T> : IObservable<T> where T : IMessage
	{
		private readonly IConsume<T> _innerConsumer;
		private readonly Subject<T> _subject = new Subject<T>();

		public ObservableConsumerWrapper (IConsume<T> consumer)
		{
			Ensure.NotNull (consumer, "consumer");

			_innerConsumer = consumer;
		}

		public IDisposable Subscribe (IObserver<T> observer)
		{
			throw new NotImplementedException ();
		}
	}
}

