using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.Entidades.DTOs;
using TesteDesenvolvedorKria.Entidades.Interfaces;

namespace TesteDesenvolvedorKria.DAO.TransacoesPedagiosDAO
{
    public class TransacoesPedagioDAO(IDbContext _bancoDeDados, ILogger<Servico> _logger) : ITransacoesPedagioDAO
    {
        private readonly IMongoCollection<TabTransacoes> _tabTransacoes = _bancoDeDados.ObterColecao<TabTransacoes>(nameof(TabTransacoes));
        public async Task<IEnumerable<TabTransacoes>> ObterRegistros()
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Empty;
                IEnumerable<TabTransacoes> transacoesDePedagios = await _tabTransacoes.Find(_ => true).ToListAsync();

                _logger.LogInformation("Registros obtidos com sucesso!");
                return transacoesDePedagios;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocorreu um erro ao tentar obter a lista de transações de pedágio. Erro: {ex?.InnerException?.Message}");
                return [];
            }
        }
    }
}
