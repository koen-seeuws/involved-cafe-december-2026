using Microsoft.AspNetCore.Mvc;
using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Abstractions.Queries.GetAllToDos;

namespace MyToDoList.Presentation.WebApi.Controllers;

/// <summary>Everybody's to-dos (read-only).</summary>
[ApiController]
[Route("api/todos")]
[Produces("application/json")]
public class ToDosController : ControllerBase
{
    private readonly IQueryHandler<GetAllToDosQuery, IEnumerable<ToDoDto>> _getAllToDos;

    public ToDosController(IQueryHandler<GetAllToDosQuery, IEnumerable<ToDoDto>> getAllToDos)
    {
        _getAllToDos = getAllToDos;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<ToDoDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ToDoDto>>> GetAll(CancellationToken ct)
        => Ok(await _getAllToDos.HandleAsync(new GetAllToDosQuery(), ct));
}
