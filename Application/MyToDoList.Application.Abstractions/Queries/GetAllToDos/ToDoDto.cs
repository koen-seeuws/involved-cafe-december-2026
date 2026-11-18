namespace MyToDoList.Application.Abstractions.Queries.GetAllToDos;

public record ToDoDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Owner { get; init; }
}