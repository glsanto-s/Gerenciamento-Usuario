using GerenciamentoUsuario.Domain.Entities;

namespace GerenciamentoUsuario.Domain.Adapters
{
    public interface IKafkaProducer
    {
        Task ProducerNotify(User user);
    }
}
