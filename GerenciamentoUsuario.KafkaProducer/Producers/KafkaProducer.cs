using Confluent.Kafka;
using GerenciamentoUsuario.Domain.Adapters;
using GerenciamentoUsuario.Domain.Entities;
using Newtonsoft.Json;

namespace GerenciamentoUsuario.MSK.Producers
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly ProducerConfig _config;
        private readonly string _topic;
        public KafkaProducer(ProducerConfig config, string topic) 
        {
            _config = config;
            _topic = topic;
        }

        public async Task ProducerNotify(User user)
        {
            using(var producer =  new ProducerBuilder<Null,string>(_config).Build())
            {
                var message = new Message<Null, string>
                {
                    Value = JsonConvert.SerializeObject(user)
                };

                try
                {
                    var result = await producer.ProduceAsync(_topic, message);
                    Console.WriteLine($"Mensagem enviada em {result.TopicPartitionOffset}");
                }
                catch(ProduceException<Null,string> ex)
                {
                    Console.WriteLine($"Erro no envio da mensagem: {ex.Error.Reason}");
                }
            }
        }
    }
}
