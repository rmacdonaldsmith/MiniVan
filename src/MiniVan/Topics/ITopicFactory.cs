using System;
using System.Collections.Generic;

namespace MiniVan.Topics
{
	public interface ITopicFactory<T>
	{
		IList<string> GetTopicsFor (T item);
	}
}

