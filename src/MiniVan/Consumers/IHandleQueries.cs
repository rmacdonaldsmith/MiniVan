using MiniVan.Bus;

namespace MiniVan.Consumers
{
	public interface IHandleQueries<in TRequest, out TResponse> where TRequest : IRequest<TResponse>
	{
		TResponse Handle(TRequest request);
	}
}

