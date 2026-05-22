using Microsoft.EntityFrameworkCore;
using TaskFlow.ApplicatonDbContext;
using TaskFlow.Dtos.AuthDto;
using TaskFlow.Entites;
using TaskFlow.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthService(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var userExists = await _context.Users
            .AnyAsync(x => x.Email == dto.Email);

        if (userExists)
            throw new Exception("User already exists");

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = dto.Password // (later we hash it)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _jwtService.GenerateToken(user);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null || user.PasswordHash != dto.Password)
            throw new Exception("Invalid credentials");

        return _jwtService.GenerateToken(user);
    }
}