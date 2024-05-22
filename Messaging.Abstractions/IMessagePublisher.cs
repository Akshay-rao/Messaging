namespace Messaging.Abstractions;

public interface IMessagePublisher<T> : IDisposable
{
    void Publish(T message);
}