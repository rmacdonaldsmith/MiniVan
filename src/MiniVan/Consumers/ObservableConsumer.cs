using System;
//using System.Reactive;
//using System.Reactive.Disposables;
//using System.Reactive.Linq;
//using System.Threading.Tasks;
//using System.Reactive.Subjects;
using MiniVan.Bus;

namespace MiniVan.Consumers
{
	public class ObservableConsumerWrapper<T> : IObservable<T> where T : IMessage
	{
		private readonly IConsume<T> _innerConsumer;
		//private readonly Subject<T> _subject = new Subject<T>();

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

