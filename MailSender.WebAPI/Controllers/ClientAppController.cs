using MailSender.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.WebAPI.Controllers;

[ApiController]
[Route("client-app")]
public class ClientAppController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ClientAppController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterClientRequestDto request)
    {
        var validPasswords = _configuration.GetSection("Registration:Passwords").Get<List<string>>();

        if (!validPasswords.Contains(request.Pass))
        {
            return StatusCode(403, new
            {
                error = "Invalid index-based password XX"
            });
        }

        return Ok(new
        {
            message = "Registration endpoint works",
            request.AppId,
            request.AppName
        });
    }
}