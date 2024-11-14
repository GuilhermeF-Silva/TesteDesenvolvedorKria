using MongoDB.Driver;

namespace TesteDesenvolvedorKria.DAO.Conexao
{
    public interface IDbContext
    {
        IMongoDatabase BancoDeDados { get; set; }

        IMongoCollection<T> ObterColecao<T>(string nomeColecao);
    }
}