using Cqrs.Abstraction;
using CqrsTestConsole.Commands;
using CqrsTestConsole.Commands.Result;

namespace CqrsTestConsole.Handlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> HandleAsync(CreateUserCommand command)
    {
        // Simulate user creation logic
        await Task.Delay(100); // Simulate async work

        return new CreateUserResult
        {
            Success = true,
            Message = $"User '{command.UserName}' with email '{command.Email}' created successfully."
        };
    }
}