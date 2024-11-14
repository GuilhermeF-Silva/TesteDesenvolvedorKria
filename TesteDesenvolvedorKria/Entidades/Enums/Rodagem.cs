using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class Rodagem
    {
        private enum RodagemEnum
        { 
            Simples,
            Dupla
        }

        public static string RealizarConversaoParaEnvio(int id)
        {
            return id switch
            {
                1 => RodagemEnum.Simples.ToString(),
                2 => RodagemEnum.Dupla.ToString(),
                _ => throw new ArgumentException("ID inválido para conversão"),
            };
        }
    }
}
