﻿using Messaging.Abstractions;
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

        var message = new SampleMessage { SampleProperty1 = "Value1" };
        publisher.Publish(message);

        Console.ReadLine();
        ((IDisposable)serviceProvider).Dispose();
    }


    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMessagePublisher<SampleMessage>>(sp => new RabbitMqPublisher<SampleMessage>("localhost", "demo_queue"));
    }
}

public class SampleMessage
{
    public string SampleProperty1 { get; set; }
}