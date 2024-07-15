using Cqrs;
using Cqrs.Abstraction;
using CqrsTestConsole.Commands;
using CqrsTestConsole.Commands.Result;
using CqrsTestConsole.Handlers;
using CqrsTestConsole.Queries;
using CqrsTestConsole.Queries.Result;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsTestConsole;

public static class CqrsDependencyInjectionExtension
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddTransient<ICommandHandler<CreateUserCommand, CreateUserResult>, CreateUserCommandHandler>();
        services.AddTransient<IQueryHandler<GetUserQuery, User>, GetUserQueryHandler>();
        services.AddSingleton<IMediator, Mediator>();
        return services;
    }
}