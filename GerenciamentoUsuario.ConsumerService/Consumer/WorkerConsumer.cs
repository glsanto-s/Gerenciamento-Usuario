using Confluent.Kafka;
using GerenciamentoUsuario.Domain;
using GerenciamentoUsuario.Domain.Entities;
using GerenciamentoUsuario.Domain.Services;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace GerenciamentoUsuario.ConsumerService.Consumer
{
    public class WorkerConsumer : BackgroundService
    {
        private readonly ConsumerConfig _config;
        private readonly KafkaTopic _topic;
        private readonly IServiceScopeFactory _scopeFactory;

        public WorkerConsumer(
            ConsumerConfig config,
            KafkaTopic topic,
            IServiceScopeFactory scopeFactory)
        {
            _config = config;
            _topic = topic;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(_config).Build())
            {
                consumer.Subscribe(_topic.Topic);
                Console.WriteLine($"Consumindo tópico {_topic.Topic}");

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Aguardando menssagem..");
                        var message = consumer.Consume(cancellationToken);
                        if (message != null)
                        {
                            Console.WriteLine($"Mensagem recebida: {message.Value} ");
                            var userJson = JsonConvert.DeserializeObject<dynamic>(message.Value);
                            var user = new User
                            {
                                Id = ObjectId.Parse((string)userJson!.Id),
                                Name = (string)userJson.Name,
                                Email = (string)userJson.Email,
                                Password = (string)userJson.Password
                            };

                            using (var scope = _scopeFactory.CreateScope())
                            {
                                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                                var subject = $"Bem Vindo {user.Name}!";
                                var body = $"Seu cadastro foi realizado com sucesso em nosso site!";

                                await emailService.SendEmailAsync(user.Email, subject, body);
                                Console.WriteLine($"Email enviado para {user.Email}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhuma menssagem recebida por enquanto!");
                        }
                    }
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Ocorreu um erro: {ex.Error.Reason}");
                }
                consumer.Close();
            }
        }
    }
}
