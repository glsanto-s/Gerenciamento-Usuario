using GerenciamentoUsuario.Domain.Adapters;
using GerenciamentoUsuario.Domain.Entities;
using MongoDB.Driver;

namespace GerenciamentoUsuario.DataBase.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _db;
        public UserRepository(IMongoClient client, string dataBase, string collection)
        {
            var db = client.GetDatabase(dataBase);
            _db = db.GetCollection<User>(collection);
        }
        public async Task<User> CreateUser(User user)
        {
            try
            {
                var validationEmail = await GetUserByEmail(user.Email);
                if (validationEmail != null)
                {
                    throw new Exception("Email já cadastrado!");
                }
                await _db.InsertOneAsync(user);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<User>> GetAllUsuarios()
        {
            var response = await _db.Find(u => true).ToListAsync();
            return response;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var response = await _db.Find(u => u.Email == email).FirstOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void TestConnection()
        {
            try
            {
                // Teste a conexão tentando acessar a coleção de usuários
                _db.Find(_ => true).Limit(1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
            }
        }
    }
}
