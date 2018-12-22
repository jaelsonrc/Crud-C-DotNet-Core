using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Rua
    {
        public Rua(string cep, string nome)
        {
            Cep = cep;
            Nome = nome;
        }

        string Cep { get; set; }
        string Nome { get; set; }
        

        public override bool Equals(object obj) => obj is Rua o && Equals(o);

        public bool Equals(Rua other) => Cep == other.Cep && Nome == other.Nome;

        public override int GetHashCode() => HashCode.Combine(Cep, Nome);

        public override string ToString()
        {
            return $"Nome: {Nome}, CEP: {Cep}";
        }

    }
}
