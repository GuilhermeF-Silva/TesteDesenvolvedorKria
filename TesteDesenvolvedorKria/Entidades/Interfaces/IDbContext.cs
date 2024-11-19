using MongoDB.Driver;

namespace TesteDesenvolvedorKria.Entidades.Interfaces
{
    public interface IDbContext
    {
        IMongoDatabase BancoDeDados { get; set; }

        IMongoCollection<T> ObterColecao<T>(string nomeColecao);
    }
}