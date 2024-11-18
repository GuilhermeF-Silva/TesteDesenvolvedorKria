using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TesteDesenvolvedorKria.Entidades.ViewModels
{
    public class DadosArquivosSaida
    {
        public string Candidato { get; set; } = "Guilherme Ferreira da Silva";
        public string DataReferencia { get; set; } = DateTime.Now.Date.ToString("dd/MM/yyyy");
        public string Motivo { get; set; } = "1";
        public string? NumeroArquivo { get; set; }
        public List<Registros>? Registros { get; set; }
    }

    public class Registros(string guid, string codigoPracaPedagio, string codigoCabine, string instante, string sentido,
                     string tipoVeiculo, string quantidadeEixosVeiculo, string rodagem, string isento, string motivoIsencao, string evasao,
                     string eixoSuspenso, string quantidadeEixosSuspensos, string tipoCobrancaEfetuada, string? placa, string liberacaoCancela,
                     string valorDevido, string valorArrecadado, string? cnpjAmap, string? veiculoCarregado, string multiplicadorTarifa, string? idtag)
    {
        public string GUID { get; set; } = guid;
        public string CodigoPracaPedagio { get; set; } = codigoPracaPedagio;
        public string CodigoCabine { get; set; } = codigoCabine;
        public string Instante { get; set; } = instante;
        public string Sentido { get; set; } = sentido;
        public string TipoVeiculo { get; set; } = tipoVeiculo;
        public string QuantidadeEixosVeiculo { get; set; } = quantidadeEixosVeiculo;
        public string Rodagem { get; set; } = rodagem;
        public string Isento { get; set; } = isento;
        public string MotivoIsencao { get; set; } = motivoIsencao;
        public string Evasao { get; set; } = evasao;
        public string EixoSuspenso { get; set; } = eixoSuspenso;
        public string QuantidadeEixosSuspensos { get; set; } = quantidadeEixosSuspensos;
        public string TipoCobrancaEfetuada { get; set; } = tipoCobrancaEfetuada;
        public string? Placa { get; set; } = placa;
        public string LiberacaoCancela { get; set; } = liberacaoCancela;
        public string ValorDevido { get; set; } = valorDevido;
        public string ValorArrecadado { get; set; } = valorArrecadado;
        public string? CnpjAmap { get; set; } = cnpjAmap;
        public string? VeiculoCarregado { get; set; } = veiculoCarregado;
        public string MultiplicadorTarifa { get; set; } = multiplicadorTarifa;
        public string? IDTAG { get; set; } = idtag;
    }
}
