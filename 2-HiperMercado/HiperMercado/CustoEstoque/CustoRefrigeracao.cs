using System;
using System.Collections.Generic;
using System.Text;

namespace HiperMercado.CustoEstoque
{
    class CustoRefrigeracao : ICusto
    {
        public double CalcularCusto(Item item)
        {
            if (item.Refrigerado)
                return item.Custo * 0.05;
            else
                return 0.0;
        }
    }
}
