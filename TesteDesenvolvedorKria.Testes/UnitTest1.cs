using AutoFixture;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using TesteDesenvolvedorKria.BLL;
using TesteDesenvolvedorKria.DAO.TransacoesPedagiosDAO;
using TesteDesenvolvedorKria.Entidades.DTOs;
using TesteDesenvolvedorKria.Entidades.Interfaces;

namespace TesteDesenvolvedorKria.Testes
{
    public class UnitTest1
    {
        private readonly Mock<ITransacoesPedagioDAO> _mockTransacoesPedagioDAO;
        private readonly Mock<IDbContext> _mockDbContext;
        private readonly Mock<ILogger<Servico>> _mockLogger;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly TransacoesPedagiosBLL _transacoesPedagiosBLL;
        private readonly TransacoesPedagioDAO _transacoesPedagiosDAO;
        private readonly IFixture _fixture;

        public UnitTest1()
        {
            // Mocking the dependencies
            _mockDbContext = new Mock<IDbContext>();
            _mockLogger = new Mock<ILogger<Servico>>();

            // Mockando a propriedade BancoDeDados para retornar uma instância do IMongoDatabase
            var mockDatabase = new Mock<IMongoDatabase>();
            _mockDbContext.Setup(x => x.BancoDeDados).Returns(mockDatabase.Object);

            // Criando a instância do DAO com as dependências mockadas
            _transacoesPedagiosDAO = new TransacoesPedagioDAO(_mockDbContext.Object, _mockLogger.Object);

            // Configurando o serviço BLL
            _mockTransacoesPedagioDAO = new Mock<ITransacoesPedagioDAO>();
            _transacoesPedagiosBLL = new TransacoesPedagiosBLL(_mockTransacoesPedagioDAO.Object, _mockLogger.Object);
        }


        [Fact]
        public void TestaRegistrosInValidos()
        {
            var transacoesMock = new List<TabTransacoes>
            {
                new TabTransacoes
                {
                    Id = ObjectId.GenerateNewId(),
                    IdTransacao = 12345,
                    DtCriacao = DateTime.Now,
                    CodigoPracaPedagio = "001",
                    CodigoCabine = 10,
                    Instante = "2024-11-19 10:00:00",
                    Sentido = 2,  
                    QuantidadeEixosVeiculo = 2,
                    Rodagem = 2,
                    Isento = 2,
                    MotivoIsencao = 2,
                    Evasao = 2,
                    EixoSuspenso = 2,
                    QuantidadeEixosSuspensos = 2,
                    TipoCobranca = 2,
                    Placa = "ABC1234",  
                    LiberacaoCancela = 2,
                    ValorDevido = 20.50m,
                    ValorArrecadado = 20.50m,
                    CnpjAmap = null,
                    MultiplicadorTarifa = "2.5",
                    VeiculoCarregado = 0,
                    IdTag = "ID12345"
                },
               new TabTransacoes
               {
                    Id = ObjectId.GenerateNewId(),
                    IdTransacao = 12345,
                    DtCriacao = DateTime.Now,
                    CodigoPracaPedagio = "001",
                    CodigoCabine = 10,
                    Instante = "2024-11-19 10:00:00",
                    Sentido = 1,
                    QuantidadeEixosVeiculo = 2,
                    Rodagem = 1,
                    Isento = 1,
                    MotivoIsencao = 2,
                    Evasao = 1,
                    EixoSuspenso =2,
                    QuantidadeEixosSuspensos = 2,
                    TipoCobranca = 1,
                    Placa = null,
                    LiberacaoCancela = 1,
                    ValorDevido = 20.50m,
                    ValorArrecadado = 20.50m,
                    CnpjAmap = "12345678000195",
                    MultiplicadorTarifa = "2.5",
                    VeiculoCarregado = 2,
                    IdTag = "ID12345"
               },
            };


            var transacoesPedagioService = _transacoesPedagiosBLL.RetornaTransacoesDePedagioTratadosParaEnvio(transacoesMock);
            
            Assert.Empty(transacoesPedagioService);

        }
    }
}