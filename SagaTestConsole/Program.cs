using Microsoft.Extensions.DependencyInjection;
using Saga.Abstractions;
using Saga;
using SagaTestConsole.Context;
using SagaTestConsole.Steps;

namespace SagaTestConsole;

public class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSaga<OrderContext>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var sagaCoordinator = serviceProvider.GetRequiredService<ISagaCoordinator<OrderContext>>();

        var createOrderStep = new CreateOrderStep();
        var reserveInventoryStep = new ReserveInventoryStep();

        var orderSaga = sagaCoordinator as SagaCoordinator<OrderContext>;
        orderSaga.AddStep(createOrderStep);
        orderSaga.AddStep(reserveInventoryStep);

        var orderContext = new OrderContext();

        try
        {
            await orderSaga.ExecuteAsync(orderContext);
            Console.WriteLine("Saga completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Saga failed: {ex.Message}");
        }
    }
}