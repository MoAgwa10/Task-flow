using System.ComponentModel.DataAnnotations;

public class UpdateProjectDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}