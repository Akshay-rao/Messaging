using Cqrs.Abstraction;
using CqrsTestConsole.Commands.Result;

namespace CqrsTestConsole.Commands;

public class CreateUserCommand : ICommand<CreateUserResult>
{
    public string UserName { get; set; }
    public string Email { get; set; }
}