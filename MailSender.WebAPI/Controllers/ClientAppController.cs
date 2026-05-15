using MailSender.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.WebAPI.Controllers;

[ApiController]
[Route("client-app")]
public class ClientAppController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterClientRequestDto request)
    {
        return Ok(new
        {
            message = "Registration endpoint works",
            request.AppId,
            request.AppName
        });
    }
}