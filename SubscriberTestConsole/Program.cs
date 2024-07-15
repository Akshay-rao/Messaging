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

        var subscriber = serviceProvider.GetService<IMessageSubscriber<SampleMessage>>();


        var messageStream = subscriber.GetMessageStream();
        var subscription = messageStream.Subscribe(m => HandleMessage(m));
        Console.ReadLine();
        subscription.Dispose();
        ((IDisposable)serviceProvider).Dispose();
    }

    private static void HandleMessage(SampleMessage receivedMessage)
    {
        Console.WriteLine($"Processed: {receivedMessage.SampleProperty1}");

    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMessageSubscriber<SampleMessage>>(sp => new RabbitMqSubscriber<SampleMessage>("localhost", "demo_queue"));
    }
}

public class SampleMessage
{
    public string SampleProperty1 { get; set; }
}