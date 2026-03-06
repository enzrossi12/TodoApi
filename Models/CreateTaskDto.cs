using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class CreateTaskDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

}
