namespace Messaging.Abstractions
{
    public interface IMessageSubscriber<out T> : IDisposable
    {
        IObservable<T?> GetMessageStream();
    }
}
