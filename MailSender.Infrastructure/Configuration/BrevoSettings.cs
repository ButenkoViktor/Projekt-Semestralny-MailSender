namespace MailSender.Infrastructure.Configuration;

public class BrevoSettings
{
    public string ApiUrl { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string SenderEmail { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;
}