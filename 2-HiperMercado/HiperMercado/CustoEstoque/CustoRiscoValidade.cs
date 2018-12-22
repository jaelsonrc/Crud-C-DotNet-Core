using System;
using System.Collections.Generic;
using System.Text;

namespace HiperMercado.CustoEstoque
{
    class CustoRiscoValidade : ICusto
    {
        public double CalcularCusto(Item item)
        {
            if(item.Validade > 30)
                return item.Custo * 0.1;
            else
                return item.Custo * 0.2;

        }
    }
}
