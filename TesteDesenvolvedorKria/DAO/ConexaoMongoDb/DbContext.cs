using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.DAO.Conexao
{
    public class DbContext : IDbContext
    {

        public IMongoDatabase BancoDeDados { get; set; }

        public DbContext(IConfiguration _configuracao)
        {
            var stringDeDConexao = _configuracao.GetConnectionString("DefaultConnection");
            var conexaoMongoDb = MongoUrl.Create(stringDeDConexao);
            var mongoClient = new MongoClient(conexaoMongoDb);

            BancoDeDados = mongoClient.GetDatabase(conexaoMongoDb.DatabaseName);
        }

        public IMongoCollection<T> ObterColecao<T>(string nomeColecao)
        {
            return BancoDeDados.GetCollection<T>(nomeColecao);
        }

    }
}
