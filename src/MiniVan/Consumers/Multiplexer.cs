using System;
using System.Collections.Generic;
using MiniVan.Bus;

namespace MiniVan.Consumers
{
    public sealed class Multiplexer<T> : IConsume<T> where T : IMessage
    {
        private readonly List<IConsume<T>> _consumers = new List<IConsume<T>>();

        public Multiplexer()
        {
            //empty default constructor
        }

        public Multiplexer(params IConsume<T>[] consumers)
        {
            if (consumers == null) throw new ArgumentNullException("consumers");
            _consumers.AddRange(consumers);
        }

        public void Attach(IConsume<T> consumer)
        {
            _consumers.Add(consumer);
        }

        public void Handle(T message)
        {
            _consumers.ForEach(x => x.Handle(message));
        }
    }
}
