using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.Entidades.Enums;

namespace TesteDesenvolvedorKria.BLL.Helpers
{
    public static class IdTagHelper
    {
        public static string? TrataIdTAG(int tipoCobranca, string? idTAG)
        {
            if (Cobranca.ValidaSeTipoCobrancaIsManual(tipoCobranca))
            { 
                return string.Empty;
            }
            return idTAG;
        }
    }
}
