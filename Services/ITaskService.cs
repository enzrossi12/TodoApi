namespace TodoApi.Services;
using TodoApi.Models;

public interface ITaskService
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Create(string title, string description);
    bool Delete(int id);
    TaskItem? Update(int id, string? title, string? description, TaskStatus? status);
}
