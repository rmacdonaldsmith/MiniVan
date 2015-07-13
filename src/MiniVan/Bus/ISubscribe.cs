using MiniVan.Consumers;

namespace MiniVan.Bus
{
	/// <summary>
	/// Implementors of this interface must handle the subscription of consumers to specific message types.
	/// Primarily used by the Bus to allow components (instances of IConsume<T>) to subscribe to specific message types.
	/// </summary>
    public interface ISubscribe
    {
		void Subscribe<T>(IConsume<T> handler) where T : class, IMessage;
    }
}
