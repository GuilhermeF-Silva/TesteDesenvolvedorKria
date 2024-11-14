using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class MotivoIsencao
    {
        private enum MotivoIsencaoEnum
        {
            [Display(Name = "Isentado concessionária")]
            IsentadoConcessionaria = 1,
            [Display(Name = "Isento por lei resolução")]
            IsentoPorLeiResolucao = 2,
            [Display(Name = "Isento Judicial")]
            IsentoJudicial = 3,
            [Display(Name = "Isento Contrato")]
            IsentoContrato = 4
        }

        public static string RealizarConversaoParaEnvio(int id)
        {
            return id switch
            {
                1 => EnumHelper.GetDisplayName(MotivoIsencaoEnum.IsentadoConcessionaria),
                2 => EnumHelper.GetDisplayName(MotivoIsencaoEnum.IsentoPorLeiResolucao),
                3 => EnumHelper.GetDisplayName(MotivoIsencaoEnum.IsentoJudicial),
                4 => EnumHelper.GetDisplayName(MotivoIsencaoEnum.IsentoContrato),
                _ => string.Empty,
            }; ;
        }

        public static bool ValidaSeMotivoIsLeiResolucaoOuJudicial(string motivoIsencao)
        {
            return motivoIsencao.Equals(MotivoIsencaoEnum.IsentoPorLeiResolucao.ToString()) || motivoIsencao.Equals(MotivoIsencaoEnum.IsentoJudicial.ToString());
        }
    }
}
