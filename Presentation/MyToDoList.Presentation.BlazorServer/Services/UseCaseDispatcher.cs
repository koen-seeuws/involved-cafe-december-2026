using MyToDoList.Application.Abstractions;
using MyToDoList.Presentation.BlazorServer.Security;

namespace MyToDoList.Presentation.BlazorServer.Services;

/// <summary>
/// Executes commands and queries from Blazor components.
/// </summary>
/// <remarks>
/// A Blazor Server circuit lives as long as the browser tab, so services resolved from it (like the
/// <c>DbContext</c>) would be long-lived and shared by concurrent UI events. Every invocation therefore
/// runs in its own DI scope, giving each command/query a fresh unit of work — like an HTTP request in the Web API.
/// </remarks>
public sealed class UseCaseDispatcher
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly CurrentUser _currentUser;

    public UseCaseDispatcher(IServiceScopeFactory scopeFactory, CurrentUser currentUser)
    {
        _scopeFactory = scopeFactory;
        _currentUser = currentUser;
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand
    {
        await using var scope = CreateScope();

        await scope.ServiceProvider
            .GetRequiredService<ICommandHandler<TCommand>>()
            .HandleAsync(command, ct);
    }

    public async Task<TReply> QueryAsync<TQuery, TReply>(TQuery query, CancellationToken ct = default)
        where TQuery : IQuery<TReply>
    {
        await using var scope = CreateScope();

        return await scope.ServiceProvider
            .GetRequiredService<IQueryHandler<TQuery, TReply>>()
            .HandleAsync(query, ct);
    }

    private AsyncServiceScope CreateScope()
    {
        var scope = _scopeFactory.CreateAsyncScope();

        if (_currentUser.UserName is { } userName)
            scope.ServiceProvider.GetRequiredService<OperationUserContext>().UserName = userName;

        return scope;
    }
}
