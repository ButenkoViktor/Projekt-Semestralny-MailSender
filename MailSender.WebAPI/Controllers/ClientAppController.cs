using MailSender.Application.DTOs;
using MailSender.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.WebAPI.Controllers;

[ApiController]
[Route("client-app")]
public class ClientAppController : ControllerBase
{
    private readonly IConfiguration _configuration;
       private readonly IJwtTokenService _jwt;

    public ClientAppController(IConfiguration configuration, IJwtTokenService jwt)
    {
        _configuration = configuration;
        _jwt = jwt;
    }

[HttpPost("register")]
public IActionResult Register([FromBody] RegisterClientRequestDto request)
    {
        var validPasswords = _configuration.GetSection("Registration:Passwords").Get<List<string>>();

        if (!validPasswords.Contains(request.Pass))
        {
            return StatusCode(403, new
            {
                error = "Invalid index-based password XX"
            });
        }

        var token = _jwt.GenerateToken(request.AppId, request.AppName);

        return Ok(new
        {
            appId = request.AppId,
            appName = request.AppName,
            key = token
        });
    }
}