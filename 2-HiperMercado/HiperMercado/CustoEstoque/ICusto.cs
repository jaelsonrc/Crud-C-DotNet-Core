using System;
using System.Collections.Generic;
using System.Text;

namespace HiperMercado.CustoEstoque
{
    public interface ICusto
    {
         double CalcularCusto(Item item);
    }
}
