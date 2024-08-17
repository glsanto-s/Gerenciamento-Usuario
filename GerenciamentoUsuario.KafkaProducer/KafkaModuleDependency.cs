using Confluent.Kafka;
using GerenciamentoUsuario.Domain;
using GerenciamentoUsuario.Domain.Adapters;
using GerenciamentoUsuario.MSK.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace GerenciamentoUsuario.MSK
{
    public static class KafkaModuleDependency
    {
        public static void AddKafkaModule(this IServiceCollection service, IConfiguration configuration)
        {
            var bootstrap = configuration["KafkaSettings:KafkaBootstrap"];
            var topic = configuration["KafkaSettings:KafkaTopic"];
            var groupId = configuration["KafkaSettings:KafkaGroupId"];

            service.AddScoped<IKafkaProducer>(provider => {
                var config = new ProducerConfig { BootstrapServers = bootstrap };
                return new KafkaProducer(config, topic!);
            });

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrap,
                GroupId = groupId!,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            service.AddSingleton(consumerConfig);
            service.AddSingleton(new KafkaTopic
            {
                Topic = topic!
            });
        }

    }
}
