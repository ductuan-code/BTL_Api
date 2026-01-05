using Gateway.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/internal/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto dto)
    {
        var user = _service.Login(dto);
        if (user == null) return Unauthorized();

        return Ok(user);
    }
}
