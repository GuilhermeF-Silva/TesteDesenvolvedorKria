using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class Cancela
    {
        private enum LiberacaoCancelaEnum
        { 
            Não = 1,
            [Display(Name = "Por atingir fila máxima prevista em contrato")]
            AtingirFilaMaxima,
            [Display(Name = "Por caso fortuito ou força maior")]
            CasoFortuitoOuForcaMaior,
        }

        public static string RealizarConversaoParaEnvio(int id)
        {
            return id switch
            {
                1 => LiberacaoCancelaEnum.Não.ToString(),
                2 => EnumHelper.GetDisplayName(LiberacaoCancelaEnum.AtingirFilaMaxima),
                3 => EnumHelper.GetDisplayName(LiberacaoCancelaEnum.CasoFortuitoOuForcaMaior),
                _ => throw new ArgumentException("ID inválido para conversão"),
            }; ;
        }

        private static bool ValidaSeLiberacaoIsDiferenteDeNao(int liberacaoCancela)
        {
            return liberacaoCancela == (int)LiberacaoCancelaEnum.AtingirFilaMaxima || liberacaoCancela == (int)LiberacaoCancelaEnum.CasoFortuitoOuForcaMaior;
        }

        public static string TrataValorArrecadado(string valorMonetario, int liberacaoCancela)
        {
            if (ValidaSeLiberacaoIsDiferenteDeNao(liberacaoCancela))
            {
                return valorMonetario.ToString();
            }
            return string.Empty;
        }


    }
}
