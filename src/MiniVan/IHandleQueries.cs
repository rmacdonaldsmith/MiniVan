namespace MiniVan
{
	public interface IHandleQueries<in TRequest, out TResponse> where TRequest : IRequest<TResponse>
	{
		TResponse Handle(TRequest request);
	}
}

