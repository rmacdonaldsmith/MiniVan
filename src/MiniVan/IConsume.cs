namespace MiniVan

{
	/// <summary>
	/// Implementors handle messages of type T
	/// </summary>
	public interface IConsume<in T> where T : IMessage
    {
        void Handle(T msg);
    }
}
