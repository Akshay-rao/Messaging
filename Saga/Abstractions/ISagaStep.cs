namespace Saga.Abstractions
{
    public interface ISagaStep<TContext>
    {
        Task ExecuteAsync(TContext context);
        Task CompensateAsync(TContext context);
    }
}
