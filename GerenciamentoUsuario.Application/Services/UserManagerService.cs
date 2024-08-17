using GerenciamentoUsuario.Domain.Adapters;
using GerenciamentoUsuario.Domain.Entities;
using GerenciamentoUsuario.Domain.Services;

namespace GerenciamentoUsuario.Application.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUserRepository _userRepository;
        private readonly IKafkaProducer _kafkaProducer;

        //Construtor
        public UserManagerService(IUserRepository userRepository, IKafkaProducer kafkaProducer)
        {
            _userRepository = userRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<User> RegisterUser(User user)
        {
            var registro  = await _userRepository.CreateUser(user);
            await _kafkaProducer.ProducerNotify(registro);
            return  registro ;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var returnList = await _userRepository.GetAllUsuarios();
            return returnList ;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var returnUser = await _userRepository.GetUserByEmail(email);
            return returnUser ;
        }

        public void TesteConexao()
        {
            _userRepository.TestConnection();
        }
    }
}
