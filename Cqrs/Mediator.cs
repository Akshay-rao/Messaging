using Cqrs.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            if (handler == null)
                throw new InvalidOperationException($"Command handler for '{typeof(TCommand).Name}' not registered.");

            return await handler.HandleAsync(command);
        }

        public async Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            if (handler == null)
                throw new InvalidOperationException($"Query handler for '{typeof(TQuery).Name}' not registered.");

            return await handler.HandleAsync(query);
        }
    }
}
