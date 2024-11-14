using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.Entidades.Enums;

namespace TesteDesenvolvedorKria.BLL.Helpers
{
    public static class PlacaHelper
    {
        public static bool ValidaPlaca(string motivoIsencao, string tipoCobranca, string? placa)
        {
            if (MotivoIsencao.ValidaSeMotivoIsLeiResolucaoOuJudicial(motivoIsencao) && string.IsNullOrEmpty(placa))
            {
                return false;
            }

            if (Cobranca.ValidaSeCobrancaIsAutomatica(tipoCobranca) && string.IsNullOrEmpty(placa))
            {
                return false;
            }

            return true;
        }
            
    }
}
