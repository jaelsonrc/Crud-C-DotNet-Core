using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
          
            /*
            * Utilizando obrigatoriamente um Dictionary em C# implemente um´método 
            * que receba uma List<Casa> e devolva uma List<Rua> ordenada de maneira decrescente 
            * pelo total de eleitores. Adicione às classes, os métodos que julgar necessários
            * 
            * 
            */
            List<Casa> listaDeCasa = new List<Casa>();
            listaDeCasa.Add(new Casa(new Rua("8888", "A"), 54, 10));
            listaDeCasa.Add(new Casa(new Rua("7777", "b"), 10, 15));
            listaDeCasa.Add(new Casa(new Rua("99999", "c"), 9, 20));
            listaDeCasa.Add(new Casa(new Rua("55555", "d"), 54, 25));
            listaDeCasa.Add(new Casa(new Rua("55555", "d"), 55, 25));


            Console.WriteLine("Lista de Rua para visistar");
             var ruasPorTotalEleitores = GetRuaOrdenadaPeloTotalEleitores(listaDeCasa);
            ruasPorTotalEleitores.Select(e=>$"{e.Key.ToString()} Total Eleitores: {e.Value}").ToList().ForEach(Console.WriteLine);
                       
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("ruas para visitar");
            GetRuaOrdenadaPeloTotalEleitores(ruasPorTotalEleitores).ForEach(Console.WriteLine);

            Console.ReadLine();
        }

   


        public static IList<KeyValuePair<Rua, int>> GetRuaOrdenadaPeloTotalEleitores(IList<Casa> casas)
        {
            IDictionary<Rua, int> mapRuaTotalEleitores = new Dictionary<Rua, int>();
            foreach (var casa in casas)
            {
                Rua rua = casa.Rua;
                
                if (mapRuaTotalEleitores.TryGetValue(rua, out int eleitores))
                {
                    
                    mapRuaTotalEleitores[rua] = eleitores + casa.TotalEleitores;
                }
                else
                {
                    mapRuaTotalEleitores.Add(rua, casa.TotalEleitores);
                }
            }

            return (from entidade in mapRuaTotalEleitores
                    orderby entidade.Value descending
                    select entidade).ToList();

        }

        public static List<Rua> GetRuaOrdenadaPeloTotalEleitores(IList<KeyValuePair<Rua, int>> ruasPorTotalEleitores)
                    => ruasPorTotalEleitores.Select(e => e.Key).ToList();
    }
}
