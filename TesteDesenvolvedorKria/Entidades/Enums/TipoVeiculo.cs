using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDesenvolvedorKria.Entidades.Enums
{
    public static class TipoVeiculo
    {
        private enum TipoVeiculoEnum 
        {
            Passeio = 1,
            Comercial,
            Moto
        }

        public static string RealizarDePara(int quantidadeEixos, int rodagem)
        {
            return (quantidadeEixos, rodagem) switch
            {
                (2,1) => TipoVeiculoEnum.Moto.ToString(),
                (2, 2) => TipoVeiculoEnum.Passeio.ToString(),
                _ => TipoVeiculoEnum.Comercial.ToString(),
            };
        }

        public static bool ValidaSeTipoVeiculoIsComercial(int quantidadeEixos, int rodagem)
        {
            return quantidadeEixos != 2 && rodagem >= 2 ;
        }

        public static bool ValidaSeTipoVeiculoIsMoto(int quantidadeEixos, int rodagem)
        {
            return quantidadeEixos == 2 && rodagem == 1;
        }

        public static bool ValidaSeTipoVeiculoIsPasseio(int quantidadeEixos, int rodagem)
        {
            return quantidadeEixos == 2 && rodagem == 2;
        }
    }
}
