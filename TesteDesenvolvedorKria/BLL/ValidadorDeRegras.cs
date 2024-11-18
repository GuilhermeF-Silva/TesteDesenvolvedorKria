using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.Entidades.DTOs;
using TesteDesenvolvedorKria.Entidades.Enums;

namespace TesteDesenvolvedorKria.BLL
{
    public static class ValidadorDeRegras
    {
        public static bool VerificaSeTransacaoContemplaRegras(TabTransacoes transacoesPedagio)
        {

            if (ValidaObrigatoriedadeDaPlaca(transacoesPedagio))
            {
                return false;
            }

            if (ValidaObrigatoriedadeCnpjAmap(transacoesPedagio))
            {
                return false;
            }

            if (ValidaObrigatoriedadeVeiculoCarregado(transacoesPedagio)) 
            { 
                return false; //VERIFICAR SE VALE A PENA CONSIDERAR OS EIXOS SUSPENSOS
            }

            if (ValidadObrigatoriedadeIdTAG(transacoesPedagio))
            {
                return false; 
            }

            return ValidaValoresDeTarifa(transacoesPedagio);
        }

        private static bool ValidaObrigatoriedadeDaPlaca(TabTransacoes transacoesPedagio)
        {
            if (MotivoIsencao.ValidaSeMotivoIsLeiResolucaoOuJudicial((int)transacoesPedagio.MotivoIsencao!) && string.IsNullOrEmpty(transacoesPedagio.Placa) ||
                   Cobranca.ValidaSeCobrancaIsAutomatica((int)transacoesPedagio.TipoCobranca!) && string.IsNullOrEmpty(transacoesPedagio.Placa))
            {
                return true;
            }
            return false;
        }

        private static bool ValidaObrigatoriedadeCnpjAmap(TabTransacoes transacoesPedagio) 
        {
            if (Cobranca.ValidaSeCobrancaIsAutomatica((int)transacoesPedagio.TipoCobranca!) && !Evasao.ValidaSeHouveEvasao(transacoesPedagio.Evasao) && string.IsNullOrEmpty(transacoesPedagio.CnpjAmap))
            {
                return true;
            }
            return false;
        }

        private static bool ValidaObrigatoriedadeVeiculoCarregado(TabTransacoes transacoesPedagio)
        {
            if (!TipoVeiculo.ValidaSeTipoVeiculoIsComercial(transacoesPedagio.QuantidadeEixosVeiculo, transacoesPedagio.Rodagem) && transacoesPedagio.VeiculoCarregado is null)
            {
                return true; //VERIFICAR SE VALE A PENA CONSIDERAR OS EIXOS SUSPENSOS
            }
            return false;
        }

        private static bool ValidadObrigatoriedadeIdTAG(TabTransacoes transacoesPedagio)
        {
            if (Cobranca.ValidaSeCobrancaIsAutomatica((int)transacoesPedagio.TipoCobranca!) && !Evasao.ValidaSeHouveEvasao(transacoesPedagio.Evasao) && string.IsNullOrEmpty(transacoesPedagio.IdTag))
            {
                return true;
            }
            return false;
        }

        private static bool ValidaValoresDeTarifa(TabTransacoes transacoesPedagio)
        {
            if (TipoVeiculo.ValidaSeTipoVeiculoIsMoto(transacoesPedagio.QuantidadeEixosVeiculo, transacoesPedagio.Rodagem) && !Isencao.ValidaSeIsIsento(transacoesPedagio.Isento))
            {
                return Convert.ToDecimal(transacoesPedagio.MultiplicadorTarifa) == Convert.ToDecimal("0.5");
            }
            if (TipoVeiculo.ValidaSeTipoVeiculoIsMoto(transacoesPedagio.QuantidadeEixosVeiculo, transacoesPedagio.Rodagem) && Isencao.ValidaSeIsIsento(transacoesPedagio.Isento))
            {
                return Convert.ToDecimal(transacoesPedagio.MultiplicadorTarifa) == Convert.ToDecimal("0.0");
            }
            if (TipoVeiculo.ValidaSeTipoVeiculoIsPasseio(transacoesPedagio.QuantidadeEixosVeiculo, transacoesPedagio.Rodagem))
            {
                return Convert.ToDecimal(transacoesPedagio.MultiplicadorTarifa) == Convert.ToDecimal("1.0") && Convert.ToDecimal(transacoesPedagio.MultiplicadorTarifa) == Convert.ToDecimal("1.5") && Convert.ToDecimal(transacoesPedagio.MultiplicadorTarifa) == Convert.ToDecimal("2.0");
            }

            if (TipoVeiculo.ValidaSeTipoVeiculoIsComercial(transacoesPedagio.QuantidadeEixosVeiculo, transacoesPedagio.Rodagem))
            {
                return Convert.ToDecimal(transacoesPedagio.MultiplicadorTarifa) >= Convert.ToDecimal("2.0") && Convert.ToDecimal(transacoesPedagio.MultiplicadorTarifa) <= Convert.ToDecimal("20.0");
            }
            return false;
        }
    }
}
