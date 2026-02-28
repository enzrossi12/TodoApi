using TodoApi.Models;

namespace TodoApi.Services;

public class TaskService{

    private readonly List<TaskItem> _tasks = new ();
    private int _nextId = 1;

}