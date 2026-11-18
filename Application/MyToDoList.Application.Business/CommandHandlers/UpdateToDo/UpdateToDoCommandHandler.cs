using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Commands.UpdateToDo;
using MyToDoList.Application.Abstractions.Persistence;
using MyToDoList.Core.Domain;

namespace MyToDoList.Application.Business.Commands.UpdateToDo;

public class UpdateToDoCommandHandler : ICommandHandler<UpdateToDoCommand>
{
    private readonly IRepository<ToDoList, string> _repository;
    
    public UpdateToDoCommandHandler(IRepository<ToDoList, string> repository)
    {
        _repository = repository;
    }
    
    public async Task HandleAsync(UpdateToDoCommand command, CancellationToken cancellationToken)
    {
        var owner = string.Empty; // TODO: retrieve owner name
        
        var toDoList = await _repository.GetAsync(owner, cancellationToken);
        
        toDoList.UpdateToDo(command.Key, command.Title, command.Description);
        
        await _repository.SaveAsync(toDoList, cancellationToken);
    }
}
