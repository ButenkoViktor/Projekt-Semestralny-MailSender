using MailSender.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.WebAPI.Controllers;

[ApiController]
[Route("mail")]
[Authorize]
public class MailController : ControllerBase
{
    [HttpPost("send")]
    public IActionResult Send([FromBody] SendMailRequestDto request)
    {
        var appId = User.FindFirst("appId")?.Value;
        var appName = User.FindFirst("appName")?.Value;

        var subject = request.Subject;
        var body = request.Body;

        if (subject.EndsWith("?"))
        {
            subject = $"[Q] {subject}";
        }

        if (body.Contains("Butenko"))
        {
            body = body.Replace(
                "Butenko",
                "[student.butenko]Butenko[/student.butenko]");
        }

        return Ok(new
        {
            appId,
            appName,
            status = "queued",
            email = new
            {
                to = request.To,
                subject,
                body
            }
        });
    }
}