namespace TodoApi.Services;
using TodoApi.Models;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(string title, string description);
    Task<bool> DeleteAsync(int id);
    Task<TaskItem?> UpdateAsync(int id, string? title, string? description, TaskStatus? status);
}
