using System.Security.Claims;

namespace TaskFlow.Extensions;

public static class UserClaimsExtensions
{
    public static string GetUserId(this ClaimsPrincipal user) =>
        user.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new UnauthorizedAccessException();

    public static bool IsAdmin(this ClaimsPrincipal user) =>
        user.IsInRole("Admin");
}
