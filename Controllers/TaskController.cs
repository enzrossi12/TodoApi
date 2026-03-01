using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;
using TodoApi.Models;
using TodoApi.Services;

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
    public IActionResult Create(CreateTaskDto dto)
    {
        var createdTask = _taskService.Create(dto.Title, dto.Description); 
        return Created($"/api/tasks/{createdTask.Id}", createdTask);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var allTasks = _taskService.GetAll();
        return Ok(allTasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById (int id)
    {
        var taskById = _taskService.GetById(id);
        if (taskById == null){
            return NotFound();
        }

        return Ok(taskById);

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deletedTask =_taskService.Delete(id);
        if (!deletedTask){
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult Update(int id, UpdateTaskDto dto)
    {
        var updatedTask = _taskService.Update(id, dto.Title, dto.Description, dto.Status);
        if (updatedTask == null){
            return NotFound();
        }
        return Ok(updatedTask);
    }
}
