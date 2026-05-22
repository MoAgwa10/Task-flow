using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Dtos.ProjectsDto
{
    public class CreateProjectDto
    {

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
