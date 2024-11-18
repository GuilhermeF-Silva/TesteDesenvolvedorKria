using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDesenvolvedorKria.Entidades.Enums;

namespace TesteDesenvolvedorKria.BLL.Helpers
{
    public static class CnpjAmapHelper
    {
        public static string? TrataCnpjAmap(int tipoCobranca, string? cnpjAmap)
        {
            if (Cobranca.ValidaSeTipoCobrancaIsManual(tipoCobranca))
            { 
                return string.Empty;
            }
            return cnpjAmap;
        }
    }
}
