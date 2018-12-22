using System;
using System.Collections.Generic;
using System.Text;

namespace HiperMercado.CustoEstoque
{

   public class CalcularCusto
    {
        private readonly IList<ICusto> custos;

        public CalcularCusto(){
            custos = new List<ICusto>();
        }

        public void AdicionarCusto(ICusto custo)
        {
            custos.Add(custo);
        }

        public double CalcularPreco(Item item)
        {
            double somaCusto = 0.0;
            foreach (var custo in custos)
            {
                somaCusto += custo.CalcularCusto(item);
            }

            return item.Custo+somaCusto;
        }
    }
}
