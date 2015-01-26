using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MiniVan.Topics
{
	public sealed class MessageTypeTopics : ITopicFactory<Type>
    {
        private readonly ConcurrentDictionary<Type, string[]> _topics = new ConcurrentDictionary<Type, string[]>();

        public IList<string> GetTopicsFor(Type type)
        {
            var topics = _topics.GetOrAdd(type, t => GetMessageTopics(t).ToArray());
            return topics.ToList();
        }

        /// <summary>
        /// Build a collection of interfaces and base types from the messageType param. 
        /// Works its way from the top down to object.
        /// </summary>
        /// <param name="messageType"></param>
        /// <returns></returns>
        private IEnumerable<string> GetMessageTopics(Type messageType)
        {
            var currentType = messageType;
            while (currentType != typeof(object))
            {
                yield return currentType.FullName;

                currentType = currentType.BaseType;
            }

            var interfaces = messageType.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                yield return @interface.FullName;
            }
        }
    }
}