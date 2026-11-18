using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Commands.DeleteToDo;
using MyToDoList.Application.Abstractions.Persistence;
using MyToDoList.Core.Domain;

namespace MyToDoList.Application.Business.Commands.DeleteToDo;

public class DeleteToDoCommandHandler : ICommandHandler<DeleteToDoCommand>
{
    private readonly IRepository<ToDoList, string> _repository;
    
    public DeleteToDoCommandHandler(IRepository<ToDoList, string> repository)
    {
        _repository = repository;
    }
    
    public async Task HandleAsync(DeleteToDoCommand command, CancellationToken cancellationToken)
    {
        var owner = string.Empty; // TODO: retrieve owner name
        
        var toDoList = await _repository.GetAsync(owner, cancellationToken);
        
        toDoList.DeleteToDo(command.Key);
        
        await _repository.SaveAsync(toDoList, cancellationToken);
    }
}
