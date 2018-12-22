using Cms.Domain.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Domain.Entities
{
    public class Cliente : BaseEntity
    {         
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String CPF { get; set; }
        public String Endereco { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public String Telefone { get; set; }
        public String Celular { get; set; }
        [JsonIgnore]
        public IList<Agenda> Agendas { get; set; }


        public Cliente()
        {

        }

        public Cliente(string nome, string sobrenome, string cPF, string endereco, string cidade, string estado, string telefone, string celular)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Endereco = endereco;
            Cidade = cidade;
            Estado = estado;
            Telefone = telefone;
            Celular = celular;
        }

        public override string ToString()
        {
            return $"Cliente: {this.Id}, {this.Nome}  {this.Sobrenome}, CPF:  {this.CPF} , Celular:  {this.Celular}";
        }

    }
}
