using HiperMercado.CustoEstoque;
using System;

namespace HiperMercado
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hipermercado Calculando custo do item:");         

            CalcularCusto calcularCusto = new CalcularCusto();

            calcularCusto.AdicionarCusto(new CustoAquisicao());
            calcularCusto.AdicionarCusto(new CustoRefrigeracao());
            calcularCusto.AdicionarCusto(new CustoRiscoValidade());
            calcularCusto.AdicionarCusto(new CustoVolumeOcupado());

            Item item = new Item("Pernil suino bauboa", 30.0, 30, 10, true);
            double precoCusto = calcularCusto.CalcularPreco(item);         
            Console.WriteLine(item);
            Console.WriteLine($"O Prço de custo é {precoCusto}");
            Console.ReadLine();
        }
    }
}
