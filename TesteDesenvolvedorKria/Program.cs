using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using TesteDesenvolvedorKria.BLL;
using TesteDesenvolvedorKria.DAO.Conexao;
using TesteDesenvolvedorKria.DAO.TransacoesPedagiosDAO;

namespace TesteDesenvolvedorKria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<ServiceConfig>(hostContext.Configuration.GetSection(nameof(ServiceConfig)));

                    services.AddSingleton<IDbContext, DbContext>();
                    services.AddSingleton<ITransacoesPedagioDAO, TransacoesPedagioDAO>();
                    services.AddSingleton<ITransacoesPedagiosBLL, TransacoesPedagiosBLL>();


                    services.AddHostedService<Servico>()
                    .Configure<EventLogSettings>(config =>
                    {
                        config.LogName = "TesteDesenvolvedorKria.Service";
                        config.SourceName = "TesteDesenvolvedorKria.Service";
                    });
                })
            .UseWindowsService();
    }
}