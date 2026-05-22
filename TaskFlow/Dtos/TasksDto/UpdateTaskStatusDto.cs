using System.ComponentModel.DataAnnotations;

public class UpdateTaskStatusDto
{
    [Required]
    public Guid TaskId { get; set; }

    [Required]
    public TaskStatus Status { get; set; }
}