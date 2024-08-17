using GerenciamentoUsuario.Domain.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace GerenciamentoUsuario.Application
{
    public class EmailService : IEmailService
    {
        private readonly Dictionary<string, string> _emailConfig;
        public EmailService(
            Dictionary<string, string> emailConfig
        ){
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var smtpServer = _emailConfig["SmtpServer"];
            var smtpPort = int.Parse(_emailConfig["SmtpPort"]);
            var smtpUser = _emailConfig["SmtpUser"];
            var smtpPass = _emailConfig["SmtpPass"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gyovanna Lima", smtpUser));
            message.Body = new TextPart("html")
            {
                Text = body
            };
            message.Subject = subject;
            message.To.Add(new MailboxAddress("", email));

            using( var cliente = new SmtpClient())
            {
                try
                {
                    cliente.Timeout = 10000;
                    Console.WriteLine("Conectando ao servidor SMTP...");
                    await cliente.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);

                    Console.WriteLine("Autenticando...");
                    await cliente.AuthenticateAsync(smtpUser, smtpPass);

                    Console.WriteLine("Enviando e-mail...");
                    await cliente.SendAsync(message);
                    Console.WriteLine("Email enviado com sucesso!");

                    await cliente.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Falha ao enviar e-mail: {ex.Message}");
                }
                
            }
        }
    }
}
