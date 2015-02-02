using System;
using System.Collections.Generic;

namespace MiniVan.Topics
{
	public interface ITopicFactory<T>
	{
		IEnumerable<string> GetTopicsFor (T item);
	}
}

