using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class UpdateTaskDto : IValidatableObject
{
    [MinLength(3)]
    [MaxLength(100)]
    public string? Title { get; set; } 

    [MinLength(3)]
    [MaxLength(500)]
    public string? Description { get; set; }

    public TaskStatus? Status { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Title is null && Description is null && Status is null)
        {
            yield return new ValidationResult(
                "Envie pelo menos um campo para atualizar: title, description ou status.",
                new[] { nameof(Title), nameof(Description), nameof(Status) });
        }
    }
}
