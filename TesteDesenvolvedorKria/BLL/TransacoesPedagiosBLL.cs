using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.BLL.Helpers;
using TesteDesenvolvedorKria.DAO.TransacoesPedagiosDAO;
using TesteDesenvolvedorKria.Entidades.DTOs;
using TesteDesenvolvedorKria.Entidades.Enums;
using TesteDesenvolvedorKria.Entidades.ViewModels;
using static TesteDesenvolvedorKria.Entidades.ViewModels.DadosArquivosSaida;

namespace TesteDesenvolvedorKria.BLL
{
    public class TransacoesPedagiosBLL(ITransacoesPedagioDAO _transacoesPedagioDAO) : ITransacoesPedagiosBLL
    {
        public async Task IniciarProcessamento()
        {
            IEnumerable<TabTransacoes> listaDeTransacoesDePedagio = await _transacoesPedagioDAO.ObterRegistros();

            if (listaDeTransacoesDePedagio.Any())
            {
                TrataDadosDeTransacoesDePedagio(listaDeTransacoesDePedagio);
            }
        }

        public static void TrataDadosDeTransacoesDePedagio(IEnumerable<TabTransacoes> listaDeTransacoesDePedagio)
        {
            IEnumerable<Registros> listaDeRegistrosSaida;

            foreach (var transacaoDePedagio in listaDeTransacoesDePedagio)
            {
                try
                {
                    Registros registroSaida = new(
                    guid: Guid.NewGuid().ToString("D").ToLower(),
                    codigoPracaPedagio: transacaoDePedagio.CodigoPracaPedagio,
                    codigoCabine: transacaoDePedagio.CodigoCabine.ToString(),
                    instante: transacaoDePedagio.Instante,
                    sentido: Sentidos.RealizarConversaoParaEnvio(transacaoDePedagio.Sentido),
                    tipoVeiculo: TipoVeiculo.RealizarConversaoParaEnvio(transacaoDePedagio.QuantidadeEixosVeiculo, transacaoDePedagio.Rodagem),
                    quantidadeEixosVeiculo: transacaoDePedagio.QuantidadeEixosVeiculo.ToString(),
                    rodagem: Rodagem.RealizarConversaoParaEnvio(transacaoDePedagio.Rodagem),
                    isento: Isencao.RealizarConversaoParaEnvio(transacaoDePedagio.Isento),
                    motivoIsencao: Isencao.TrataMotivoIsencao(MotivoIsencao.RealizarConversaoParaEnvio(transacaoDePedagio.MotivoIsencao), transacaoDePedagio.Isento), 
                    evasao: Evasao.RealizarConversaoParaEnvio(transacaoDePedagio.Evasao),
                    eixoSuspenso: Eixo.RealizarConversaoParaEnvio(transacaoDePedagio.EixoSuspenso),
                    quantidadeEixosSuspensos: Eixo.TrataQuantidadeEixosSuspensos(transacaoDePedagio.QuantidadeEixosSuspensos, transacaoDePedagio.EixoSuspenso),
                    tipoCobrancaEfetuada: Cobranca.RealizarConversaoParaEnvio(transacaoDePedagio.TipoCobranca),
                    placa: transacaoDePedagio.Placa, 
                    liberacaoCancela: Cancela.RealizarConversaoParaEnvio(transacaoDePedagio.LiberacaoCancela),
                    valorDevido: UtilsBLL.FormatarValorMonetario(transacaoDePedagio.ValorDevido),
                    valorArrecadado: Cancela.TrataValorArrecadado(UtilsBLL.FormatarValorMonetario(transacaoDePedagio.ValorArrecadado), transacaoDePedagio.LiberacaoCancela),
                    cnpjAmap: transacaoDePedagio.CnpjAmap,
                    multiplicadorTarifa: UtilsBLL.FormatarValorMultiplicadorTarifa(transacaoDePedagio.MultiplicadorTarifa), 
                    veiculoCarregado: VeiculoCarregado.RealizarConversaoParaEnvio(transacaoDePedagio.VeiculoCarregado),
                    idtag: IdTagHelper.TrataIdTAG(transacaoDePedagio.TipoCobranca, transacaoDePedagio.IdTag));
                }
                catch (Exception ex)
                {

                    throw ;
                }  
            }     
        }
        public static bool ValidaRegras(Registros registroSaida)
        {
           return PlacaHelper.ValidaPlaca(registroSaida.MotivoIsencao, registroSaida.TipoCobrancaEfetuada, registroSaida.Placa);
        }
    }
}
