using TodoApi.Models;
using System.Linq;
using TaskStatus = TodoApi.Models.TaskStatus;
namespace TodoApi.Services;

public class TaskService : ITaskService{

    private readonly List<TaskItem> _tasks = new ();
    private int _nextId = 1;

    public Task<TaskItem> CreateAsync(string title, string description){

        var task = new TaskItem(title, description);
        task.Id = _nextId;
        _nextId++;
        _tasks.Add(task);
        return Task.FromResult(task);

    }

    public Task<List<TaskItem>> GetAllAsync()
    {
        return Task.FromResult(_tasks.ToList());
    }

    public Task<TaskItem?> GetByIdAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null){
        return Task.FromResult(false);
        }
        _tasks.Remove(task);

        return Task.FromResult(true); 
    }

    public Task<TaskItem?> UpdateAsync(int id, string? title, string? description, TaskStatus? status)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if(task == null){
            return Task.FromResult<TaskItem?>(null);
        }
        if (title != null) {
            task.Title = title;
        } 
        if (description != null) {
            task.Description = description;
        } 
        if (status.HasValue) {
            task.Status = status.Value;
        }

        return Task.FromResult<TaskItem?>(task); 
    }
}
