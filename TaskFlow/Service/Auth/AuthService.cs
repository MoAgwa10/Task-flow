using Microsoft.AspNetCore.Identity;
using TaskFlow.Dtos.AuthDto;
using TaskFlow.Entites;

namespace TaskFlow.Service.Auth;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _jwt;

    public AuthService(UserManager<ApplicationUser> userManager, JwtService jwt)
    {
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) != null)
            throw new Exception("User already exists");

        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "User");
        var roles = await _userManager.GetRolesAsync(user);
        return _jwt.GenerateToken(user, roles);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            throw new Exception("Invalid credentials");

        var roles = await _userManager.GetRolesAsync(user);
        return _jwt.GenerateToken(user, roles);
    }
}
