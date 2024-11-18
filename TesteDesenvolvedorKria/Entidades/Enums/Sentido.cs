using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class Sentidos
    {
        private enum SentidoEnum
        {
            Crescente = 1,
            Decrescente
        }

        public static string RealizarDePara(int id)
        {
            return id switch
            {
                1 => SentidoEnum.Crescente.ToString(),
                2 => SentidoEnum.Decrescente.ToString(),
                _ => throw new ArgumentException("ID inválido para conversão"),
            };
        }
    }
}
