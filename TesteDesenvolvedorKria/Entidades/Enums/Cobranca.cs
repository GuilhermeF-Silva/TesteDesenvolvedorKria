using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public class Cobranca
    {
        private enum TipoCobrancaEfetuadaEnum
        {
            Manual = 1,
            [Display(Name = "Automática TAG")]
            AutomaticaTAG,
            [Display(Name = "Automática OCR/PLACA")]
            AutomaticaOCR_PLACA
        }

        public static string RealizarConversaoParaEnvio(int id)
        {
            return id switch
            {
                1 => TipoCobrancaEfetuadaEnum.Manual.ToString(),
                2 => EnumHelper.GetDisplayName(TipoCobrancaEfetuadaEnum.AutomaticaTAG),
                3 => EnumHelper.GetDisplayName(TipoCobrancaEfetuadaEnum.AutomaticaOCR_PLACA),
                _ => throw new ArgumentException("ID inválido para conversão"),
            };
        }

        public static bool ValidaSeTipoCobrancaIsManual(int tipoCobranca)
        { 
            return tipoCobranca == (int)TipoCobrancaEfetuadaEnum.Manual;
        }

        public static bool ValidaSeCobrancaIsAutomatica(string cobranca)
        {
            return cobranca.Equals(TipoCobrancaEfetuadaEnum.AutomaticaTAG.ToString()) || cobranca.Equals(TipoCobrancaEfetuadaEnum.AutomaticaOCR_PLACA.ToString());
        }
    }
}
