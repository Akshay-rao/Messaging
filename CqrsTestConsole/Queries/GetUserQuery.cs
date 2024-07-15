using Cqrs.Abstraction;
using CqrsTestConsole.Queries.Result;

namespace CqrsTestConsole.Queries;

public class GetUserQuery : IQuery<User>
{
    public string UserName { get; set; }
}