namespace GerenciamentoUsuario.Domain.Adapters
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }
    }
}
