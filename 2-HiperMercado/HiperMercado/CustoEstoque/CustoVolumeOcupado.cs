using System;
using System.Collections.Generic;
using System.Text;

namespace HiperMercado.CustoEstoque
{
    class CustoVolumeOcupado : ICusto
    {
        public double CalcularCusto(Item item)
        {
            if(item.Volume>50)
                return item.Custo * 0.2;
            else
                return item.Custo * 0.05;
        }
    }
}
