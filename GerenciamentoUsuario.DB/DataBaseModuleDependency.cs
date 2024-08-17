using GerenciamentoUsuario.DataBase.Repositories;
using GerenciamentoUsuario.Domain.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace GerenciamentoUsuario.DataBase
{
    public static class DataBaseModuleDependency
    {
        public static void AddDataBaseModule(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            services.AddSingleton<IMongoClient>(mongoClient);

            services.AddScoped<IUserRepository>(provider =>
                new UserRepository(provider.GetRequiredService<IMongoClient>(), "client", "user")
            );
        }
    }
}
