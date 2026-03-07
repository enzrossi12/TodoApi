using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var createdTask = await _taskService.CreateAsync(dto.Title, dto.Description); 
        return Created($"/api/tasks/{createdTask.Id}", createdTask);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var allTasks = await _taskService.GetAllAsync();
        return Ok(allTasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var taskById = await _taskService.GetByIdAsync(id);
        if (taskById == null)
        {
            return NotFound(new
            {
                code = "TASK_NOT_FOUND",
                message = "Tarefa nao encontrada.",
                id
            });
        }

        return Ok(taskById);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedTask = await _taskService.DeleteAsync(id);
        if (!deletedTask)
        {
            return NotFound(new
            {
                code = "TASK_NOT_FOUND",
                message = "Tarefa nao encontrada.",
                id
            });
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var updatedTask = await _taskService.UpdateAsync(id, dto.Title, dto.Description, dto.Status);
        if (updatedTask == null)
        {
            return NotFound(new
            {
                code = "TASK_NOT_FOUND",
                message = "Tarefa nao encontrada.",
                id
            });
        }
        return Ok(updatedTask);
    }
}
