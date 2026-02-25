using TodoApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<TaskItem> tasks = new List<TaskItem>();
//endpoints here

app.MapPost("/tasks", (CreateTaskDto dto) => 
{
    var task = new TaskItem(dto.Title, dto.Description);
    tasks.Add(task);
    return Results.Created("/tasks", task);
});
app.Run();

