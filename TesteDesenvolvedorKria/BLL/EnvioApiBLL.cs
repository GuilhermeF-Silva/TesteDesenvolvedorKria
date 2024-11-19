using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.Entidades.ViewModels;

namespace TesteDesenvolvedorKria.BLL
{
    public class EnvioApiBLL()
    {
        private static readonly string _url = "https://contratacaosirapi.azurewebsites.net/Candidato/PublicarDesafio";
        public static DadosArquivosSaida MontaJsonParaEnvio(List<Registros> listaDeRegistrosSaida, int numeroArquivos)
        {
            var arquivosSaida = new DadosArquivosSaida()
            {
                NumeroArquivo = numeroArquivos.ToString(),
                Registros = listaDeRegistrosSaida
            };

            return arquivosSaida;
        }

        public static async Task EnviarArquivosParaAPI(DadosArquivosSaida arquivosSaida)
        {
            using (HttpClient client = new())
            {

                string jsonPayload = System.Text.Json.JsonSerializer.Serialize(arquivosSaida);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(_url, content);
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao tentar enviar os dados para a api: {ex.Message}");
                }
            }
        }
    }
}
