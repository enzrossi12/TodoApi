namespace TodoApi.Models;

public class UpdateTaskDto
{
    public string? Title { get; set; } 
    public string? Description { get; set; }
    public TaskStatus? Status { get; set; }
}