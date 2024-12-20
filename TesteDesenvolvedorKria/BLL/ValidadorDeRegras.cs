﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                return false; 
            }

            if (ValidadObrigatoriedadeIdTAG(transacoesPedagio))
            {
                return false; 
            }

            return true;
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
            if (!TipoVeiculo.ValidaSeTipoVeiculoIsComercial(transacoesPedagio.QuantidadeEixosVeiculo, transacoesPedagio.Rodagem, double.Parse(transacoesPedagio.MultiplicadorTarifa.Trim(), CultureInfo.InvariantCulture)) && transacoesPedagio.VeiculoCarregado is null)
            {
                return true; 
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
    }
}
