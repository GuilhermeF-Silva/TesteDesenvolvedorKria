using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class Evasao
    {
        private enum EvasaoEnum
        {
            Sim = 1, 
            Não
        }
        
        public static string RealizarDePara(int id)
        {
            return id switch
            {
                1 => EvasaoEnum.Sim.ToString(),
                2 => EvasaoEnum.Não.ToString(),
                _ => throw new ArgumentException("ID inválido para conversão"),
            };
        }

        public static bool ValidaSeHouveEvasao(int evasao)
        {
            return evasao == (int)EvasaoEnum.Sim;
        }

    }
}
