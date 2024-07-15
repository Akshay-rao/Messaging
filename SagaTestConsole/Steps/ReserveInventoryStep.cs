using Saga.Abstractions;
using SagaTestConsole.Context;

namespace SagaTestConsole.Steps;

public class ReserveInventoryStep : ISagaStep<OrderContext>
{
    public async Task ExecuteAsync(OrderContext context)
    {
        Console.WriteLine("Reserving inventory...");
        // Simulate inventory reservation logic
        await Task.Delay(100);
        context.InventoryReserved = true;
    }

    public async Task CompensateAsync(OrderContext context)
    {
        Console.WriteLine($"Compensating for inventory reservation, OrderId: {context.OrderId}");
        // Simulate compensation logic for inventory reservation
        await Task.Delay(100);
        context.InventoryReserved = false;
    }
}