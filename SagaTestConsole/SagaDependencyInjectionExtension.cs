using Microsoft.Extensions.DependencyInjection;
using Saga;
using Saga.Abstractions;

namespace SagaTestConsole;

public static class SagaDependencyInjectionExtension
{
    public static IServiceCollection AddSaga<TContext>(this IServiceCollection services)
    {
        services.AddSingleton<ISagaCoordinator<TContext>, SagaCoordinator<TContext>>();
        return services;
    }
}