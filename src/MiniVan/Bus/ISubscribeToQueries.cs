using MiniVan.Consumers;

namespace MiniVan.Bus
{
	public interface ISubscribeToQueries
	{
		void Subscribe<TRequest, TResponse> (IHandleQueries<TRequest, TResponse> queryhandler) where TRequest : IRequest<TResponse>;
	}
}

