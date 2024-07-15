using Cqrs.Abstraction;
using CqrsTestConsole.Queries;
using CqrsTestConsole.Queries.Result;

namespace CqrsTestConsole.Handlers;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
    public async Task<User> HandleAsync(GetUserQuery query)
    {
        // Simulate user retrieval logic
        await Task.Delay(100); // Simulate async work

        return new User { UserName = query.UserName, Email = $"{query.UserName}@example.com" };
    }
}