using TesteDesenvolvedorKria.Entidades.DTOs;

namespace TesteDesenvolvedorKria.Entidades.Interfaces
{
    public interface ITransacoesPedagioDAO
    {
        Task<IEnumerable<TabTransacoes>> ObterRegistros();
    }
}