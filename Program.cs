using TodoApi.Models;
using System.Text.Json.Serialization;
using System.Linq;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigureHttpJsonOptions( options => 
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
List<TaskItem> tasks = new List<TaskItem>();
var nextId = 1;
//endpoints here
app.MapGet("/tasks", () => Results.Ok(tasks));

app.MapGet("/tasks/{id}", (int id) => 
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task == null){
        return Results.NotFound();
    } 
    return Results.Ok(task);
});

app.MapPost("/tasks", (CreateTaskDto dto) => 
{
    var task = new TaskItem(dto.Title, dto.Description);
    task.Id = nextId;
    nextId++;
    tasks.Add(task);
    return Results.Created("/tasks", task);
});

app.MapDelete("/tasks/{id}", (int id) => 
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task == null){
        return Results.NotFound();
    }
    tasks.Remove(task);
    return Results.NoContent(); 
});

app.MapPatch("/tasks/{id}", (int id, UpdateTaskDto dto) => 
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if(task == null){
        return Results.NotFound();
    }   

    if (dto.Title != null){
        task.Title = dto.Title;
    }

    if (dto.Description != null){
        task.Description = dto.Description;
    } 

    if (dto.Status.HasValue){
    task.Status = dto.Status.Value;
    }   

    return Results.Ok(task);
});
app.Run();

