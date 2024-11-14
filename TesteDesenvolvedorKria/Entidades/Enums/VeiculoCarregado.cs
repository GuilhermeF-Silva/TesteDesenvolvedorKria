using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class VeiculoCarregado
    {
        private enum VeiculoCarregadoEnum
        { 
            Vazio = 1,
            Carregado,
            [Display(Name = " Não identificado")]
            NaoIdentificado
        }

        public static string? RealizarConversaoParaEnvio(int? id)
        {
            return id switch
            {
                1 => VeiculoCarregadoEnum.Vazio.ToString(),
                2 => VeiculoCarregadoEnum.Carregado.ToString(),
                3 => EnumHelper.GetDisplayName(VeiculoCarregadoEnum.NaoIdentificado),
                _ => null,
            };
        }


    }
}
