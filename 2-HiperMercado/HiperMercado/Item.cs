using System;
using System.Collections.Generic;
using System.Text;

namespace HiperMercado
{
    public class Item
    {

        public String Nome { get; private set; }
        public double Custo { get; private set; }
        public int Validade { get; private set; }
        public int Volume { get; private set; }
        public bool Refrigerado { get; private set; }

        public Item(string nome, double custo, int validade, int volume, bool refrigerado)
        {
            Nome = nome;
            Custo = custo;
            Validade = validade;
            Volume = volume;
            Refrigerado = refrigerado;
        }

        public override string ToString()
        {
            return $"Nome:{Nome}, Custo: {Custo}, Validade: {Validade}, Refrigerado: {Refrigerado}";
        }
    }
}
