using System.Reactive.Linq;
using System.Text.Json;
using Messaging.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging.RabbitMq;

public class RabbitMqSubscriber<T> : IMessageSubscriber<T>
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IObservable<T?> _messageStream;
    private bool _disposed;
    public RabbitMqSubscriber(string hostname, string queueName)
    {
        var factory = new ConnectionFactory() { HostName = hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        _messageStream = Observable.FromEventPattern<BasicDeliverEventArgs>(
                h => consumer.Received += h,
                h => consumer.Received -= h)
            .Select(e => JsonSerializer.Deserialize<T>(System.Text.Encoding.UTF8.GetString(e.EventArgs.Body.ToArray())));
    }

    public IObservable<T?> GetMessageStream()
    {
        return _messageStream;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _channel.Close();
            _channel.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
        _disposed = true;
    }
}
