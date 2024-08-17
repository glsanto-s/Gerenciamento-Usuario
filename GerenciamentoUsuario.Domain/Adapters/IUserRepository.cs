using GerenciamentoUsuario.Domain.Entities;

namespace GerenciamentoUsuario.Domain.Adapters
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<List<User>> GetAllUsuarios();
        Task<User> GetUserByEmail(string email);
        void TestConnection();
    }
}
