using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Dtos.AuthDto;
using TaskFlow.Service.Auth;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/v1/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    public async Task<ActionResult<object>> Register(RegisterDto dto)
    {
        var token = await _auth.RegisterAsync(dto);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<ActionResult<object>> Login(LoginDto dto)
    {
        var token = await _auth.LoginAsync(dto);
        return Ok(new { token });
    }
}
