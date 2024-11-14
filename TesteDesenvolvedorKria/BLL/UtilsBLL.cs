using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.BLL
{
    public static class UtilsBLL
    {
        public static DateTime FormatarDataString(string data) 
        {
            return DateTime.Parse(data);
        }

        public static string FormatarValorMonetario(decimal valor)
        {
            return valor.ToString("F").Replace('.', ',');
        }

        public static string FormatarValorMultiplicadorTarifa(string valor)
        {
            return valor.Replace(".", ",");
        }
    }
}
