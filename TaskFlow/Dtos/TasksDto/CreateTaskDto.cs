using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Dtos.TasksDto
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public PriorityLevel Priority { get; set; }

        [Required]
        public Guid ProjectId { get; set; }



    }
}
