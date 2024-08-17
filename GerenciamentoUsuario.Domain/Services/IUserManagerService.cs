using GerenciamentoUsuario.Domain.Entities;

namespace GerenciamentoUsuario.Domain.Services
{
    public interface IUserManagerService
    {
        Task<User> RegisterUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        void TesteConexao();
    }
}
