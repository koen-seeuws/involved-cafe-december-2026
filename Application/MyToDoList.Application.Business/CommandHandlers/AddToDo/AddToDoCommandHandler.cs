using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Commands.AddToDo;
using MyToDoList.Application.Abstractions.Persistence;
using MyToDoList.Core.Domain;

namespace MyToDoList.Application.Business.Commands.AddToDo;

public class AddToDoCommandHandler : ICommandHandler<AddToDoCommand>
{
    private readonly IRepository<ToDoList, string> _repository;
    
    public AddToDoCommandHandler(IRepository<ToDoList, string> repository)
    {
        _repository = repository;
    }
    
    public async Task HandleAsync(AddToDoCommand command, CancellationToken cancellationToken)
    {
        var owner = string.Empty; // TODO: retrieve owner name
        
        var toDoList = await _repository.GetAsync(owner, cancellationToken);
        
        toDoList.AddToDo(command.Title, command.Description);
        
        await _repository.SaveAsync(toDoList, cancellationToken);
    }
}