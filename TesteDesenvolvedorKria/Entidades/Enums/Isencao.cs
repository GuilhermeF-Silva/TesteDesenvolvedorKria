using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class Isencao
    {
        private enum IsentoEnum
        { 
            Sim = 1,
            Não
        }

        public static string RealizarDePara(int id)
        {
            return id switch
            {
                1 => IsentoEnum.Sim.ToString(),
                2 => IsentoEnum.Não.ToString(),
                _ => throw new ArgumentException("ID inválido para conversão"),
            };
        }

        public static bool ValidaSeIsIsento (int isencao)
        {
            return isencao == (int)IsentoEnum.Sim;
        }

       
       
    }
}
