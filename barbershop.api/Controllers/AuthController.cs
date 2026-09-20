using Microsoft.AspNetCore.Mvc;
using barbershop.domain;
using barbershop.api.DTOs;
using barbershop.api.Common;

namespace barbershop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISqlDataRepository _repository;

    public AuthController(ISqlDataRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto dto)
    {
        var user = _repository.Authenticate(dto.Username, dto.Password);
        if (user == null)
        {
            return Unauthorized(ApiResponse<string>.Fail("Invalid credentials"));
        }

        var response = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role.ToString()
        };

        return Ok(ApiResponse<UserResponseDto>.Ok(response, "Login successful"));
    }
}
