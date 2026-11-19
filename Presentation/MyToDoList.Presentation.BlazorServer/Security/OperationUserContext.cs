using MyToDoList.Application.Abstractions;

namespace MyToDoList.Presentation.BlazorServer.Security;

/// <summary>
/// The <see cref="IUserContext"/> for a single use-case invocation.
/// Filled in by the <see cref="Services.UseCaseDispatcher"/> from the <see cref="CurrentUser"/> of the circuit.
/// </summary>
public sealed class OperationUserContext : IUserContext
{
    private string? _userName;

    public string UserName
    {
        get => _userName ?? throw new UnauthorizedAccessException("Sign in before working with your to-do list.");
        set => _userName = value;
    }
}
