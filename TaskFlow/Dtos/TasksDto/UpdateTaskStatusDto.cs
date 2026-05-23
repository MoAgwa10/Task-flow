using System.ComponentModel.DataAnnotations;

public class UpdateTaskStatusDto
{
    [Required]
    public Guid TaskId { get; set; }

    [Required]
    public TaskFlow.Enums.TaskStatus Status { get; set; }

}