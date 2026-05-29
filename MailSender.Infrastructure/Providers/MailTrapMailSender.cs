using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MailSender.Application.Interfaces;
using MailSender.Infrastructure.Configuration;

namespace MailSender.Infrastructure.Providers;

public class MailTrapMailSender : IMailSenderProvider
{
    private readonly HttpClient _httpClient;
    private readonly MailTrapSettings _settings;

    public MailTrapMailSender(
        HttpClient httpClient,
        IOptions<MailTrapSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Add(
            "Api-Token",
            _settings.ApiKey);

        var payload = new
        {
            from = new
            {
                email = _settings.FromEmail,
                name = "MailSender"
            },

            to = new[]
            {
                new { email = to }
            },

            subject = subject,
            text = body
        };

        var json = JsonSerializer.Serialize(payload);

        var response = await _httpClient.PostAsync(
            "https://send.api.mailtrap.io/api/send",
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"));

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                $"MailTrap error: {responseContent}");
        }
    }
}