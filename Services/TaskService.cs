using TodoApi.Models;
using System.Linq;
using TaskStatus = TodoApi.Models.TaskStatus;
namespace TodoApi.Services;

public class TaskService{

    private readonly List<TaskItem> _tasks = new ();
    private int _nextId = 1;

    public TaskItem Create(string title, string description){

        var task = new TaskItem(title, description);
        task.Id = _nextId;
        _nextId++;
        _tasks.Add(task);
        return task;

    }

    public IEnumerable<TaskItem> GetAll()
    {
        return _tasks.ToList();
    }

    public TaskItem? GetById(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return task;
    }

    public bool Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null){
        return false;
        }
        _tasks.Remove(task);

        return true; 
    }

    public TaskItem? Update(int id, string? title, string? description, TaskStatus? status)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if(task == null){
            return null;
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

        return task; 
    }
}