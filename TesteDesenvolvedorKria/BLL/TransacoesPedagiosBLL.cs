using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.BLL.Helpers;
using TesteDesenvolvedorKria.Entidades.DTOs;
using TesteDesenvolvedorKria.Entidades.Enums;
using TesteDesenvolvedorKria.Entidades.ViewModels;
using System.Net.Http;
using static TesteDesenvolvedorKria.Entidades.ViewModels.DadosArquivosSaida;
using MongoDB.Bson.IO;
using Azure;
using System.Globalization;
using Microsoft.Extensions.Logging;
using DnsClient.Internal;
using TesteDesenvolvedorKria.Entidades.Interfaces;

namespace TesteDesenvolvedorKria.BLL
{
    public class TransacoesPedagiosBLL(ITransacoesPedagioDAO _transacoesPedagioDAO, ILogger<Servico> _logger) : ITransacoesPedagiosBLL
    {
        int numeroDeArquivos = 0;

        public async Task IniciarProcessamento()
        {
            _logger.LogInformation("Obtendo registros de transação de pedágio para envio...");
            IEnumerable<TabTransacoes> listaDeTransacoesDePedagio = await _transacoesPedagioDAO.ObterRegistros();

            if (listaDeTransacoesDePedagio.Any())
            {
                _logger.LogInformation("Tratando Registros para envio...");
                List<Registros> listaDeRegistrosSaida = RetornaTransacoesDePedagioTratadosParaEnvio(listaDeTransacoesDePedagio);
                
                var listeEmLotes = DividirLista(listaDeRegistrosSaida, 1000);

                for (int i = 0; i < listeEmLotes.Count; i++)
                {
                    numeroDeArquivos += listeEmLotes[i].Count;
                    DadosArquivosSaida arquivosSaida = EnvioApiBLL.MontaJsonParaEnvio(listeEmLotes[i], numeroDeArquivos);
                    _logger.LogInformation($"Enviando lote número: {i + 1}/{listeEmLotes.Count}");
                    await EnvioApiBLL.EnviarArquivosParaAPI(arquivosSaida);
                }

                _logger.LogInformation($"Registros Enviados com Sucesso!");
            }
        }

        public List<Registros> RetornaTransacoesDePedagioTratadosParaEnvio(IEnumerable<TabTransacoes> listaDeTransacoesDePedagio)
        {
            List<Registros> listaDeRegistrosSaida = [];
            List<TabTransacoes> listaDeRegistrosInvalidos= [];


            foreach (var transacaoDePedagio in listaDeTransacoesDePedagio)
            {
                try
                {
                    if (ValidadorDeRegras.VerificaSeTransacaoContemplaRegras(transacaoDePedagio))
                    {
                        Registros registroSaida = new(
                        guid: Guid.NewGuid().ToString("D").ToLower(),
                        codigoPracaPedagio: transacaoDePedagio.CodigoPracaPedagio,
                        codigoCabine: transacaoDePedagio.CodigoCabine.ToString(),
                        instante: Convert.ToDateTime(transacaoDePedagio.Instante).ToString("dd/MM/yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture),
                        sentido: Sentidos.RealizarDePara(transacaoDePedagio.Sentido),
                        tipoVeiculo: TipoVeiculo.RealizarDePara(double.Parse(transacaoDePedagio.MultiplicadorTarifa.Trim(), CultureInfo.InvariantCulture),transacaoDePedagio.QuantidadeEixosVeiculo, transacaoDePedagio.Rodagem),
                        quantidadeEixosVeiculo: transacaoDePedagio.QuantidadeEixosVeiculo.ToString(),
                        rodagem: Rodagem.RealizarDePara(transacaoDePedagio.Rodagem),
                        isento: Isencao.RealizarDePara(transacaoDePedagio.Isento),
                        motivoIsencao: MotivoIsencao.TrataMotivoIsencao(MotivoIsencao.RealizarDePara(transacaoDePedagio.MotivoIsencao), transacaoDePedagio.Isento),
                        evasao: Evasao.RealizarDePara(transacaoDePedagio.Evasao),
                        eixoSuspenso: Eixo.RealizarDePara(transacaoDePedagio.EixoSuspenso),
                        quantidadeEixosSuspensos: Eixo.TrataQuantidadeEixosSuspensos(transacaoDePedagio.QuantidadeEixosSuspensos, transacaoDePedagio.EixoSuspenso),
                        tipoCobrancaEfetuada: Cobranca.RealizarDePara(transacaoDePedagio.TipoCobranca),
                        placa: transacaoDePedagio.Placa,
                        liberacaoCancela: Cancela.RealizarDePara(transacaoDePedagio.LiberacaoCancela),
                        valorDevido: UtilsBLL.FormatarValorMonetario(transacaoDePedagio.ValorDevido),
                        valorArrecadado: Cancela.TrataValorArrecadado(UtilsBLL.FormatarValorMonetario(transacaoDePedagio.ValorArrecadado), transacaoDePedagio.LiberacaoCancela),
                        cnpjAmap: CnpjAmapHelper.TrataCnpjAmap(transacaoDePedagio.TipoCobranca, transacaoDePedagio.CnpjAmap),
                        multiplicadorTarifa: UtilsBLL.FormatarValorMultiplicadorTarifa(transacaoDePedagio.MultiplicadorTarifa),
                        veiculoCarregado: VeiculoCarregado.TrataVeiculoCarregao(VeiculoCarregado.RealizarDePara(transacaoDePedagio.VeiculoCarregado), transacaoDePedagio.QuantidadeEixosVeiculo, transacaoDePedagio.Rodagem, double.Parse(transacaoDePedagio.MultiplicadorTarifa.Trim(), CultureInfo.InvariantCulture)),
                        idtag: IdTagHelper.TrataIdTAG(transacaoDePedagio.TipoCobranca, transacaoDePedagio.IdTag));

                        listaDeRegistrosSaida.Add(registroSaida);
                    }
                    else 
                    {
                        listaDeRegistrosInvalidos.Add(transacaoDePedagio);               
                    }
                }
                catch 
                {

                    listaDeRegistrosInvalidos.Add(transacaoDePedagio);
                }  
            }
            if (listaDeRegistrosInvalidos.Count != 0)
            {
                _logger.LogError($"Lista de registros que nao seguem as regras da ANTT:");
                foreach (var item in listaDeRegistrosInvalidos)
                {
                    _logger.LogError($"Id: {item.Id}");
                }
            }

            return listaDeRegistrosSaida;
        }

        private static List<List<Registros>> DividirLista(List<Registros> lista, int tamanhoLote)
        {
            return lista
                .Select((item, indice) => new { item, indice })
                .GroupBy(x => x.indice / tamanhoLote)
                .Select(grupo => grupo.Select(x => x.item).ToList())
                .ToList();
        }
       
    } 
}
