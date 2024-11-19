using TesteDesenvolvedorKria.Entidades.DTOs;
using TesteDesenvolvedorKria.Entidades.ViewModels;

namespace TesteDesenvolvedorKria.Entidades.Interfaces
{
    public interface ITransacoesPedagiosBLL
    {
        Task IniciarProcessamento();
        List<Registros> RetornaTransacoesDePedagioTratadosParaEnvio(IEnumerable<TabTransacoes> listaDeTransacoesDePedagio);
    }
}