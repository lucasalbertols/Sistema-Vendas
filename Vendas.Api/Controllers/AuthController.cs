using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Vendas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _cfg;
    public AuthController(IConfiguration cfg) => _cfg = cfg;

    [HttpPost("token")]
    public ActionResult<string> Token([FromBody] LoginDto login)
    {
        if (login.Username != "admin" || login.Password != "123456")
            return Unauthorized("Usuário ou senha inválidos");

        var key = _cfg["Jwt:Key"] ?? "";
        var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, login.Username),
            new Claim(ClaimTypes.Role, "admin")
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(tokenString);
    }

    public record LoginDto(string Username, string Password);
}
