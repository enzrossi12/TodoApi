using TodoApi.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using TaskStatus = TodoApi.Models.TaskStatus;

namespace TodoApi.Services;

public class EfTaskService : ITaskService{

    private readonly AppDbContext _db;

    public EfTaskService(AppDbContext db){
        _db = db;
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _db.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(string title, string description)
    {
        var task = new TaskItem(title, description);
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
        {
            return false;
        }

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<TaskItem?> UpdateAsync(int id, string? title, string? description, TaskStatus? status)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
        {
            return null;
        }

        if (title != null)
        {
            task.Title = title;
        }

        if (description != null)
        {
            task.Description = description;
        }

        if (status.HasValue)
        {
            task.Status = status.Value;
        }

        await _db.SaveChangesAsync();
        return task;
    }
}
