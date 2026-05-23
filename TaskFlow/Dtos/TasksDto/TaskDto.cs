public class TaskDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskFlow.Enums.TaskStatus Status { get; set; }


    public DateTime DueDate { get; set; }

    public PriorityLevel Priority { get; set; }

    public Guid ProjectId { get; set; }
}