using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaskItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    
    public string Description { get; set; } = string.Empty;

    [Required]
    public TaskFlow.Enums.TaskStatus Status { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public PriorityLevel Priority { get; set; }

    // Foreign Key
    [Required]
    public Guid ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;
}
