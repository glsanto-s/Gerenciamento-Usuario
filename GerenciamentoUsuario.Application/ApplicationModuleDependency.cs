using GerenciamentoUsuario.Application.Services;
using GerenciamentoUsuario.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoUsuario.Application
{
    public static class ApplicationModuleDependency
    {
        public static void AddApplicationModule(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IUserManagerService, UserManagerService>();

            var emailConfig = new Dictionary<string, string>
            {
                { "SmtpServer",configuration["EmailSettings:SmtpServer"]! },
                { "SmtpPort",  configuration["EmailSettings:SmtpPort"]!},
                { "SmtpPass",  configuration["EmailSettings:SmtpPass"]!},
                { "SmtpUser",  configuration["EmailSettings:SmtpUser"]!},
            };

            service.AddSingleton(emailConfig);
            service.AddSingleton<IEmailService>(provider =>
            new EmailService(provider.GetRequiredService<Dictionary<string, string>>())
        );
        }
    }
}
