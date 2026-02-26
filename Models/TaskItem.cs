namespace TodoApi.Models;


public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TaskStatus Status { get; set; }

    public TaskItem (string title, string description)
    {
        this.Title = title;
        this.Description = description;
        Date = DateTime.Now;
        Status = TaskStatus.Pendente;;
        
    }
}

