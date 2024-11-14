using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using TesteDesenvolvedorKria.BLL;
using TesteDesenvolvedorKria.DAO.Conexao;

public class Servico(ILogger<Servico> _logger, IDbContext _bancoDeDados, ITransacoesPedagiosBLL _transacoesPedagiosBLL) : BackgroundService
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {

            if (_bancoDeDados.BancoDeDados is not null)
            { 
                _logger.LogInformation("Serviço iniciado");
            }
            else {
                _logger.LogError("Ocorreu algum erro ao tentar estabelecer a conexão com o MongoDB.");
            }
                
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar inicializar o serviço. Erro: {ex?.InnerException?.Message}");
        }

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("KEEP ALIVE", DateTimeOffset.Now);
            await Task.Run(() => _transacoesPedagiosBLL.IniciarProcessamento(), stoppingToken);
            await Task.Delay(500000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Serviço Encerrado em: {DateTime.Now}");
        await base.StopAsync(cancellationToken);
    }
}
