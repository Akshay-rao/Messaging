using Messaging.Abstractions;
using Messaging.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace TestConsole;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var publisher = serviceProvider.GetService<IMessagePublisher<SampleMessage>>();
        var subscriber = serviceProvider.GetService<IMessageSubscriber<SampleMessage>>();

        var message = new SampleMessage { SampleProperty1 = "Value1" };
        publisher.Publish(message);

        var messageStream = subscriber.GetMessageStream();
        var subscription = messageStream.Subscribe(m => HandleMessage(m));

        // Keep the subscriber alive for a while to process messages
        await Task.Delay(TimeSpan.FromSeconds(10));

        // Dispose the subscription and services
        subscription.Dispose();
        ((IDisposable)serviceProvider).Dispose();
    }

    private static void HandleMessage(SampleMessage receivedMessage)
    {
        Console.WriteLine($"Processed: {receivedMessage.SampleProperty1}");
        
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMessagePublisher<SampleMessage>>(sp => new RabbitMqPublisher<SampleMessage>("localhost", "demo_queue"));
        services.AddSingleton<IMessageSubscriber<SampleMessage>>(sp => new RabbitMqSubscriber<SampleMessage>("localhost", "demo_queue"));
    }
}

public class SampleMessage
{
    public string SampleProperty1 { get; set; }
}