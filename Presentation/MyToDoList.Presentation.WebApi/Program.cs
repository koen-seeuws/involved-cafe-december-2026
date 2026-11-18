using MyToDoList.Application.Abstractions;
using MyToDoList.Application.Business;
using MyToDoList.Infrastructure.EFC;
using MyToDoList.Presentation.WebApi;
using MyToDoList.Presentation.WebApi.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddSqlLite(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, HttpUserContext>();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // No migrations exist yet: create the schema on startup for local development.
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<ToDoDbContext>().Database.EnsureCreated();
}

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapControllers();

app.Run();
