using Saga.Abstractions;
using SagaTestConsole.Context;

namespace SagaTestConsole.Steps;

public class CreateOrderStep : ISagaStep<OrderContext>
{
    public async Task ExecuteAsync(OrderContext context)
    {
        Console.WriteLine("Creating order...");
        // Simulate order creation logic
        await Task.Delay(100);
        context.OrderId = Guid.NewGuid();
    }

    public async Task CompensateAsync(OrderContext context)
    {
        Console.WriteLine($"Compensating for order creation, OrderId: {context.OrderId}");
        // Simulate compensation logic for order creation
        await Task.Delay(100);
    }
}