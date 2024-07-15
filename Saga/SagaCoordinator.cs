using Saga.Abstractions;

namespace Saga
{
    public class SagaCoordinator<TContext> : ISagaCoordinator<TContext>
    {
        private readonly IList<ISagaStep<TContext>> _steps;

        public SagaCoordinator()
        {
            _steps = new List<ISagaStep<TContext>>();
        }

        public void AddStep(ISagaStep<TContext> step)
        {
            _steps.Add(step);
        }

        public async Task ExecuteAsync(TContext context)
        {
            var executedSteps = new Stack<ISagaStep<TContext>>();

            try
            {
                foreach (var step in _steps)
                {
                    await step.ExecuteAsync(context);
                    executedSteps.Push(step);
                }
            }
            catch
            {
                while (executedSteps.Count > 0)
                {
                    var step = executedSteps.Pop();
                    await step.CompensateAsync(context);
                }
                throw;
            }
        }
    }
}
