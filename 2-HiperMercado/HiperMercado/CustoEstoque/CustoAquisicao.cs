using System;
using System.Collections.Generic;
using System.Text;

namespace HiperMercado.CustoEstoque
{
    public class CustoAquisicao : ICusto
    {
        public double CalcularCusto(Item item)
        {
            return item.Custo * 0.1;
        }
    }
}