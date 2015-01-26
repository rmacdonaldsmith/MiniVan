using System;
using System.Collections.Generic;

namespace MiniVan.Tests.Helpers
{
    public class FakeConsumer<T> : IConsume<T> where T : IMessage
    {
		private readonly List<T> _receivedMessages;
        private readonly Action<T> _consumerDelegate;

		public FakeConsumer ()
		{
			_receivedMessages = new List<T>();
		}

        public FakeConsumer(Action<T> consumerDelegate)
        {
            _consumerDelegate = consumerDelegate;
        }

        public void Handle(T msg)
        {
			if (_consumerDelegate != null)
            	_consumerDelegate(msg);

			if (_receivedMessages != null)
				_receivedMessages.Add (msg);
        }

		public List<T> ReceivedMessages 
		{
			get { return _receivedMessages; }
		}

		public void Clear()
		{
			_receivedMessages.Clear ();
		}
    }
}
