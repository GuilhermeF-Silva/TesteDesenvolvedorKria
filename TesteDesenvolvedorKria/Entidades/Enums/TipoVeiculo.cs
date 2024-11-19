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

        public static string RealizarDePara(double multiplicadorTarifa, int quantidadeEixos, int rodagem)
        {
            if (multiplicadorTarifa == 0.5 && quantidadeEixos == 2 && rodagem == 1 || multiplicadorTarifa == 0.0 && quantidadeEixos == 2 && rodagem == 1)
            {
                return TipoVeiculoEnum.Moto.ToString();
            }
            else if (multiplicadorTarifa == 1.0 && quantidadeEixos == 2 && rodagem <= 1 || multiplicadorTarifa == 1.5 && quantidadeEixos == 2 && rodagem == 1 || multiplicadorTarifa == 2.0 && quantidadeEixos == 2 && rodagem == 1)
            { 
                return TipoVeiculoEnum.Passeio.ToString();
            }
            return TipoVeiculoEnum.Comercial.ToString();
        }

        public static bool ValidaSeTipoVeiculoIsComercial(int quantidadeEixos, int rodagem, double multiplicadorTarifa)
        {
            return quantidadeEixos >= 2 && rodagem >= 2 && multiplicadorTarifa >= 2.0;
        }

        public static bool ValidaSeTipoVeiculoIsMoto(int quantidadeEixos, int rodagem, double multiplicadorTarifa)
        {
            return quantidadeEixos == 2 && rodagem == 1 && multiplicadorTarifa == 0.0 || quantidadeEixos == 2 && rodagem == 1 && multiplicadorTarifa == 0.5;
        }

        public static bool ValidaSeTipoVeiculoIsPasseio(int quantidadeEixos, int rodagem, double multiplicadorTarifa)
        {
            return quantidadeEixos == 2 && rodagem == 1 && multiplicadorTarifa == 1.0 || quantidadeEixos == 2 && rodagem == 1 && multiplicadorTarifa == 1.5 || quantidadeEixos == 2 && rodagem == 1 && multiplicadorTarifa == 2.0;
        }
    }
}
