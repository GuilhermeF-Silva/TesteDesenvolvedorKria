using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class Eixo
    {
        private enum EixoSuspensoEnum
        { 
            Sim = 1,
            Não
        }

        public static string RealizarDePara(int id)
        {
            return id switch
            {
                1 => EixoSuspensoEnum.Sim.ToString(),
                2 => EixoSuspensoEnum.Não.ToString(),
                _ => throw new ArgumentException("ID inválido para conversão"),
            };
        }

        public static bool ValidaSePossuemAlgumEixoSuspenso(int eixoSuspenso)
        {
            return eixoSuspenso == (int)EixoSuspensoEnum.Sim;
        }

        public static string TrataQuantidadeEixosSuspensos(int quantidadeDeEixosSuspensos, int eixoSuspenso)
        {
            if (ValidaSePossuemAlgumEixoSuspenso(eixoSuspenso))
            { 
                return quantidadeDeEixosSuspensos.ToString();
            }
            return string.Empty;
        }
    }
}
