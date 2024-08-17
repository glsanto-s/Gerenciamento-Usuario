using GerenciamentoUsuario.Application;
using GerenciamentoUsuario.ConsumerService.Consumer;
using GerenciamentoUsuario.DataBase;
using GerenciamentoUsuario.MSK;

namespace GerenciamentoUsuario.ConsumerService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddApplicationModule(context.Configuration);
                    services.AddKafkaModule(context.Configuration);
                    services.AddDataBaseModule(context.Configuration);

                    services.AddHostedService<WorkerConsumer>();
                });


    }
}
