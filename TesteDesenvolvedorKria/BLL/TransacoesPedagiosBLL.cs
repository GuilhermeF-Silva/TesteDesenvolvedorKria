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
using System.Net.Http;
using static TesteDesenvolvedorKria.Entidades.ViewModels.DadosArquivosSaida;
using MongoDB.Bson.IO;
using Azure;

namespace TesteDesenvolvedorKria.BLL
{
    public class TransacoesPedagiosBLL(ITransacoesPedagioDAO _transacoesPedagioDAO) : ITransacoesPedagiosBLL
    {
        public async Task IniciarProcessamento()
        {
            IEnumerable<TabTransacoes> listaDeTransacoesDePedagio = await _transacoesPedagioDAO.ObterRegistros();

            if (listaDeTransacoesDePedagio.Any())
            {
                List<Registros> listaDeRegistrosSaida = RetornaTransacoesDePedagioTratadosParaEnvio(listaDeTransacoesDePedagio);
                var listeEmLotes = DividirLista(listaDeRegistrosSaida, 1000);

                foreach (var item in listeEmLotes)
                {
                   DadosArquivosSaida arquivosSaida =  MontaJsonParaEnvio(item);
                   await EnviarArquivosParaAPI(arquivosSaida);
                }          
            }
        }

        public static List<Registros> RetornaTransacoesDePedagioTratadosParaEnvio(IEnumerable<TabTransacoes> listaDeTransacoesDePedagio)
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
                        instante: transacaoDePedagio.Instante,
                        sentido: Sentidos.RealizarDePara(transacaoDePedagio.Sentido),
                        tipoVeiculo: TipoVeiculo.RealizarDePara(transacaoDePedagio.QuantidadeEixosVeiculo, transacaoDePedagio.Rodagem),
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
                        veiculoCarregado: VeiculoCarregado.TrataVeiculoCarregao(VeiculoCarregado.RealizarDePara(transacaoDePedagio.VeiculoCarregado), transacaoDePedagio.QuantidadeEixosVeiculo, transacaoDePedagio.Rodagem),
                        idtag: IdTagHelper.TrataIdTAG(transacaoDePedagio.TipoCobranca, transacaoDePedagio.IdTag));

                        listaDeRegistrosSaida.Add(registroSaida);
                    }
                    else 
                    {
                        listaDeRegistrosInvalidos.Add(transacaoDePedagio);               
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception(ex?.InnerException?.Message);
                }  
            }
            if (listaDeRegistrosInvalidos.Count != 0)
            {
                Console.Error.WriteLine($"Lista de registros que nao seguem as regras da ANTT: {string.Join(", ", listaDeRegistrosInvalidos)}");

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
        public static DadosArquivosSaida MontaJsonParaEnvio(List<Registros> listaDeRegistrosSaida)
        {
            var arquivosSaida = new DadosArquivosSaida()
            {
                NumeroArquivo = listaDeRegistrosSaida.Count.ToString(),
                Registros = listaDeRegistrosSaida
            };

            return arquivosSaida;
        }

        public static async Task EnviarArquivosParaAPI(DadosArquivosSaida arquivosSaida)
        {
            Console.WriteLine(arquivosSaida);
            //using (HttpClient client = new())
            //{
            //    string url = "https://contratacaosirapi.azurewebsites.net/Candidato/PublicarDesafio";

            //    string jsonPayload = System.Text.Json.JsonSerializer.Serialize(arquivosSaida);
            //    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            //    try
            //    {
            //        //var response = await client.PostAsync(url, content);
            //        //Console.WriteLine(await response.Content.ReadAsStringAsync());
            //        Console.WriteLine(arquivosSaida);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Ocorreu um erro ao tentar enviar os dados para a api: {ex.Message}");
            //    }
            //}
        }
    } 
}
