using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.DTOs
{
    public class TabTransacoes
    {
        [BsonId] 
        public required ObjectId Id { get; set; }
        public required int IdTransacao { get; set; }
        public DateTime DtCriacao { get; set; }
        public required string CodigoPracaPedagio { get; set; }
        public required int CodigoCabine { get; set; }
        public required string Instante { get; set; }
        public required int Sentido { get; set; }
        public required int QuantidadeEixosVeiculo { get; set; }
        public required int Rodagem { get; set; }
        public required int Isento { get; set; }
        public required int MotivoIsencao { get; set; }
        public required int Evasao { get; set; }
        public required int EixoSuspenso { get; set; }
        public required int QuantidadeEixosSuspensos { get; set; }
        public required int TipoCobranca { get; set; }
        public string? Placa { get; set; }
        public required int LiberacaoCancela { get; set; }
        public decimal ValorDevido { get; set; }
        public decimal ValorArrecadado { get; set; }
        public string? CnpjAmap { get; set; }
        public required string MultiplicadorTarifa { get; set; }
        public  int? VeiculoCarregado { get; set; }
        public string? IdTag { get; set; }

    }
}
