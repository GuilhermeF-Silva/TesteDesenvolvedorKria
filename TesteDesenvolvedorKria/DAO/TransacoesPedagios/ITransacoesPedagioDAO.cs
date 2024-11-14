using TesteDesenvolvedorKria.Entidades.DTOs;

namespace TesteDesenvolvedorKria.DAO.TransacoesPedagiosDAO
{
    public interface ITransacoesPedagioDAO
    {
        Task<IEnumerable<TabTransacoes>> ObterRegistros();
    }
}