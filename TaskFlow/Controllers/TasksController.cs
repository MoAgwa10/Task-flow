using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.ApplicatonDbContext;
using TaskFlow.Dtos.TasksDto;
using TaskFlow.Extensions;

namespace TaskFlow.Controllers;

using TaskStatusEnum = TaskFlow.Enums.TaskStatus;

[ApiController]
[Route("api/v1/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create(CreateTaskDto dto)
    {
        var project = await FindProjectAsync(dto.ProjectId);
        if (project == null)
            return NotFound("Project not found.");

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description ?? "",
            Status = TaskStatusEnum.Pending,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            ProjectId = dto.ProjectId
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return Ok(ToDto(task));
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<ActionResult<List<TaskDto>>> GetByProject(Guid projectId)
    {
        var project = await FindProjectAsync(projectId);
        if (project == null)
            return NotFound("Project not found.");

        var tasks = await _db.Tasks
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();

        return Ok(tasks.Select(ToDto).ToList());
    }

    [HttpPut("status")]
    public async Task<ActionResult<TaskDto>> UpdateStatus(UpdateTaskStatusDto dto)
    {
        var task = await FindTaskAsync(dto.TaskId);
        if (task == null)
            return NotFound();

        task.Status = dto.Status;
        await _db.SaveChangesAsync();

        return Ok(ToDto(task));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var task = await FindTaskAsync(id);
        if (task == null)
            return NotFound();

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private async Task<Project?> FindProjectAsync(Guid projectId)
    {
        if (User.IsAdmin())
            return await _db.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

        return await _db.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == User.GetUserId());
    }

    private async Task<TaskItem?> FindTaskAsync(Guid taskId)
    {
        if (User.IsAdmin())
        {
            return await _db.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        return await _db.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.UserId == User.GetUserId());
    }

    private static TaskDto ToDto(TaskItem t) => new()
    {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        Status = t.Status,
        DueDate = t.DueDate,
        Priority = t.Priority,
        ProjectId = t.ProjectId
    };
}
