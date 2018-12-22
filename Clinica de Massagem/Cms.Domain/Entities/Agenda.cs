using Cms.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Domain.Entities
{
   public class Agenda : BaseEntity
    {
        public Cliente Cliente { get; set; }
        public DateTime Data { get; set; }
        public int Modalidade { get; set; }
        public String Obs { get; set; }

        public Agenda()
        {
        }

        public Agenda(Cliente cliente, DateTime data, int modalidade, string obs)
        {
            Cliente = cliente;
            Data = data;
            Modalidade = modalidade;
            Obs = obs;
        }

        public override bool Equals(object obj)
        {
            Agenda agenda = obj as Agenda;
            return this==obj || (agenda != null && this.Id.Equals(agenda.Id));
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
