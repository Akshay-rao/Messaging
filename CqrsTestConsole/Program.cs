// See https://aka.ms/new-console-template for more information

namespace CqrsTestConsole;
using Microsoft.Extensions.DependencyInjection;
using Cqrs.Abstraction;
using Commands.Result;
using Commands;
using Queries.Result;
using Queries;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var serviceCollection =  new ServiceCollection();
        serviceCollection.AddCqrs();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var mediator = serviceProvider.GetRequiredService<IMediator>();

        var createUserCommand = new CreateUserCommand { UserName = "Akshay", Email = "akshay.rao@some.com" };
        var createUserResult = await mediator.SendCommandAsync<CreateUserCommand, CreateUserResult>(createUserCommand);

        Console.WriteLine(createUserResult.Message);

        var getUserQuery = new GetUserQuery { UserName = "Akshay" };
        var user = await mediator.SendQueryAsync<GetUserQuery, User>(getUserQuery);

        Console.WriteLine($"User Retrieved: {user.UserName}, {user.Email}");
    }
}