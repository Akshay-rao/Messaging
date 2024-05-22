using System.Text.Json;
using Messaging.Abstractions;
using RabbitMQ.Client;
namespace Messaging.RabbitMq;
public class RabbitMqPublisher<T> : IMessagePublisher<T>
{
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private bool _disposed;

    public RabbitMqPublisher(string hostname, string queueName)
    {
        _queueName = queueName;

        var factory = new ConnectionFactory() { HostName = hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public void Publish(T message)
    {
        var json = JsonSerializer.Serialize(message);
        var body = System.Text.Encoding.UTF8.GetBytes(json);
        _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
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
            _connection.Close();
        }

        _disposed = true;
    }
}
