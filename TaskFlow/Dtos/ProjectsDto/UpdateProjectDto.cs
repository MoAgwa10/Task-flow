using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Dtos.ProjectsDto;

public class UpdateProjectDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
