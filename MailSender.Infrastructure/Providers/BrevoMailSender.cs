using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MailSender.Application.Interfaces;
using MailSender.Infrastructure.Configuration;

namespace MailSender.Infrastructure.Providers;

public class BrevoMailSender : IMailSenderProvider
{
    private readonly HttpClient _httpClient;
    private readonly BrevoSettings _settings;

    public BrevoMailSender(
        HttpClient httpClient,
        IOptions<BrevoSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string body)
    {
        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Add(
            "api-key",
            _settings.ApiKey);

        var payload = new
        {
            sender = new
            {
                email = _settings.SenderEmail,
                name = _settings.SenderName
            },

            to = new[]
            {
                new { email = to }
            },

            subject = subject,
            htmlContent = body
        };

        var json = JsonSerializer.Serialize(payload);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            "https://api.brevo.com/v3/smtp/email",
            content);

        response.EnsureSuccessStatusCode();
    }
}