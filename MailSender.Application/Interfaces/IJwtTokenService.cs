namespace MailSender.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(string appId, string appName);
}