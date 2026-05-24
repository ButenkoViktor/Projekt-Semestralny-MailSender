using System.Text;
using System.Text.Json;
using MailSender.Application.Interfaces;
using MailSender.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace MailSender.Infrastructure.Services;

public class BrevoMailService : IMailService
{
    private readonly HttpClient _httpClient;
    private readonly BrevoSettings _settings;

    public BrevoMailService(
        HttpClient httpClient,
        IOptions<BrevoSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var requestBody = new
        {
            sender = new
            {
                name = _settings.SenderName,
                email = _settings.SenderEmail
            },

            to = new[]
            {
                new
                {
                    email = to
                }
            },

            subject = subject,
            htmlContent = body
        };

        var json = JsonSerializer.Serialize(requestBody);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Add(
            "api-key",
            _settings.ApiKey);

        var response = await _httpClient.PostAsync(
            _settings.ApiUrl,
            content);

        response.EnsureSuccessStatusCode();
    }
}