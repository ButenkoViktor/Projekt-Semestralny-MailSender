namespace MailSender.Application.DTOs;

public class RegisterClientRequestDto
{
    public string AppId { get; set; } = string.Empty;

    public string AppName { get; set; } = string.Empty;

    public string Pass { get; set; } = string.Empty;
}