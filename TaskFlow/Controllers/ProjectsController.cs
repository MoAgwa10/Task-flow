using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.ApplicatonDbContext;
using TaskFlow.Dtos.ProjectsDto;
using TaskFlow.Extensions;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/v1/projects")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProjectsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto dto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description ?? "",
            CreatedAt = DateTime.UtcNow,
            UserId = User.GetUserId()
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        return Ok(ToDto(project));
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetAll()
    {
        var query = _db.Projects.AsQueryable();

        if (!User.IsAdmin())
            query = query.Where(p => p.UserId == User.GetUserId());

        var projects = await query.ToListAsync();
        return Ok(projects.Select(ToDto).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectDto>> GetById(Guid id)
    {
        var project = await FindProjectAsync(id);
        if (project == null)
            return NotFound();

        return Ok(ToDto(project));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProjectDto>> Update(Guid id, UpdateProjectDto dto)
    {
        var project = await FindProjectAsync(id);
        if (project == null)
            return NotFound();

        project.Name = dto.Name;
        project.Description = dto.Description ?? "";

        await _db.SaveChangesAsync();
        return Ok(ToDto(project));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var project = await FindProjectAsync(id);
        if (project == null)
            return NotFound();

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private async Task<Project?> FindProjectAsync(Guid id)
    {
        if (User.IsAdmin())
            return await _db.Projects.FirstOrDefaultAsync(p => p.Id == id);

        return await _db.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == User.GetUserId());
    }

    private static ProjectDto ToDto(Project p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        CreatedAt = p.CreatedAt
    };
}
