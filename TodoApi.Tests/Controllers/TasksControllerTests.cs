using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services;
using TaskStatus = TodoApi.Models.TaskStatus;

namespace TodoApi.Tests.Controllers;

public class TasksControllerTests
{
    [Fact]
    public async Task Create_ShouldReturnCreatedResult_WithCreatedTask()
    {
        var taskServiceMock = new Mock<ITaskService>();
        taskServiceMock
            .Setup(s => s.CreateAsync("Nova", "Descricao"))
            .ReturnsAsync(new TaskItem("Nova", "Descricao") { Id = 10 });

        var controller = new TasksController(taskServiceMock.Object);

        var result = await controller.Create(new CreateTaskDto
        {
            Title = "Nova",
            Description = "Descricao"
        });

        var created = Assert.IsType<CreatedResult>(result);
        Assert.Equal("/api/tasks/10", created.Location);
        Assert.NotNull(created.Value);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WithStandardPayload_WhenTaskDoesNotExist()
    {
        var taskServiceMock = new Mock<ITaskService>();
        taskServiceMock.Setup(s => s.GetByIdAsync(5)).ReturnsAsync((TaskItem?)null);
        var controller = new TasksController(taskServiceMock.Object);

        var result = await controller.GetById(5);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var json = JsonSerializer.Serialize(notFound.Value);
        Assert.Contains("\"code\":\"TASK_NOT_FOUND\"", json);
        Assert.Contains("\"message\":\"Tarefa nao encontrada.\"", json);
        Assert.Contains("\"id\":5", json);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenTaskIsDeleted()
    {
        var taskServiceMock = new Mock<ITaskService>();
        taskServiceMock.Setup(s => s.DeleteAsync(2)).ReturnsAsync(true);
        var controller = new TasksController(taskServiceMock.Object);

        var result = await controller.Delete(2);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenTaskDoesNotExist()
    {
        var taskServiceMock = new Mock<ITaskService>();
        taskServiceMock.Setup(s => s.DeleteAsync(2)).ReturnsAsync(false);
        var controller = new TasksController(taskServiceMock.Object);

        var result = await controller.Delete(2);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var json = JsonSerializer.Serialize(notFound.Value);
        Assert.Contains("\"code\":\"TASK_NOT_FOUND\"", json);
        Assert.Contains("\"id\":2", json);
    }

    [Fact]
    public async Task Update_ShouldReturnOk_WithUpdatedTask()
    {
        var updatedTask = new TaskItem("Atualizado", "Descricao") { Id = 3, Status = TaskStatus.EmAndamento };
        var taskServiceMock = new Mock<ITaskService>();
        taskServiceMock
            .Setup(s => s.UpdateAsync(3, "Atualizado", "Descricao", TaskStatus.EmAndamento))
            .ReturnsAsync(updatedTask);
        var controller = new TasksController(taskServiceMock.Object);

        var result = await controller.Update(3, new UpdateTaskDto
        {
            Title = "Atualizado",
            Description = "Descricao",
            Status = TaskStatus.EmAndamento
        });

        var ok = Assert.IsType<OkObjectResult>(result);
        var returnedTask = Assert.IsType<TaskItem>(ok.Value);
        Assert.Equal(3, returnedTask.Id);
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenTaskDoesNotExist()
    {
        var taskServiceMock = new Mock<ITaskService>();
        taskServiceMock
            .Setup(s => s.UpdateAsync(99, null, null, null))
            .ReturnsAsync((TaskItem?)null);
        var controller = new TasksController(taskServiceMock.Object);

        var result = await controller.Update(99, new UpdateTaskDto());

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var json = JsonSerializer.Serialize(notFound.Value);
        Assert.Contains("\"code\":\"TASK_NOT_FOUND\"", json);
        Assert.Contains("\"id\":99", json);
    }
}
