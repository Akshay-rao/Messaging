namespace Saga.Abstractions;

public interface ISagaCoordinator<TContext>
{
    Task ExecuteAsync(TContext context);
    void AddStep(ISagaStep<TContext> step);
}