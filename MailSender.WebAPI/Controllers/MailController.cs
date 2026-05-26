using System.Security.Claims;
using MailSender.Application.DTOs;
using MailSender.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.WebAPI.Controllers;

[ApiController]
[Route("mail")]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;

    public MailController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [Authorize]
    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendMailRequestDto request)
    {
        var appId = User.FindFirst("appId")?.Value;
        var appName = User.FindFirst("appName")?.Value;

        var subject = request.Subject;

        if (subject.EndsWith("?"))
        {
            subject = $"[Q] {subject}";
        }

        var body = request.Body;

        if (body.Contains("Butenko"))
        {
            body = body.Replace(
                "Butenko",
                "[student.butenko]Butenko[/student.butenko]");
        }
        if (body.Contains("Slipchyshyn"))
        {
            body = body.Replace(
                "Slipchyshyn",
                "[student.Slipchyshyn]Slipchyshyn[/student.Slipchyshyn]"
            );
        }

        await _mailService.SendEmailAsync(
            request.To,
            subject,
            body
        );

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