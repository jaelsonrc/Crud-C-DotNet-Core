using Cms.Service.Helpers;
using Cms.Domain.Entities;
using Cms.Domain.Interfaces;
using Cms.Service.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace Cms.Service.Services
{
    public class AgendaService : BaseService<Agenda>, IAgendaService
    {
        private IRepository<Cliente> _repositoryCliente;
        public AgendaService(IRepository<Agenda> repository, IRepository<Cliente> repositoryCliente) : base(repository)
        {
            _repositoryCliente = repositoryCliente;
        }

        public override Agenda Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("O Id não pode ser zero");
            BaseRepository<Agenda> repo = __repository as BaseRepository<Agenda>;
            DbSet<Agenda> query = repo.DbSet as DbSet<Agenda>;
            return query.Include(c => c.Cliente).FirstOrDefault(a => a.Id == id);
        }


        /// <summary>
        /// Busca os agendamento do mes corrente
        /// </summary>
        /// <returns> Lista de Agedamento do mes corrente</returns>
        public override IList<Agenda> Get()
        {
            BaseRepository<Agenda> repo = __repository as BaseRepository<Agenda>;
            DbSet<Agenda> query = repo.DbSet as DbSet<Agenda>;
           
           // IEnumerable<Agenda> query =__repository.Get;
            List<Agenda> list = query.Include(c => c.Cliente).Where(w => w.Data.Month == DateTime.Now.Month).ToList();
   
            return list;
        }
        /// <summary>
        /// Responsavel pelo agendamento, valida os campos, busca total agendado pela soma da modalidade, a valida as regras
        /// </summary>
        /// <typeparam name="V">classe de validação</typeparam>
        /// <param name="obj">Agenda</param>
        /// <returns>Agenda</returns>
        public override Agenda Post<V>(Agenda obj)
        {

            Validate(obj, Activator.CreateInstance<V>());

            IEnumerable<Agenda> query = __repository.Get;     
            obj.Cliente = _repositoryCliente.Find(obj.Cliente.Id);   
            
            var somaModalidade=query.Where(c => c.Cliente == obj.Cliente)
               .Where(c => c.Data.Month.Equals(DateTime.Now.Month))
               .Sum(c => c.Modalidade);
        
            Validate(obj, (V)Activator.CreateInstance(typeof(V), new object[] { somaModalidade }));
            
            __repository.Incluir(obj);
            return obj;
        }


        public Agenda Post(String jsonString)
        {           
            return Post<AgendaValidator>(JsonToAgenda(jsonString));

        }
        public override Agenda Put<V>(Agenda obj)
        {
            Agenda agendaAlterar = __repository.Find(obj.Id, true);
            Validate(agendaAlterar, (V)Activator.CreateInstance(typeof(V), new object[] { true }));
            
            Validate(obj, Activator.CreateInstance<V>());

            __repository.Alterar(obj);
            return obj;
        }

        public  Agenda Put(String jsonString)
        {          
            return Put<AgendaValidator>(JsonToAgenda(jsonString));
        }
        public  void Delete(int id, string data)
        {
            DateTime dataCancelamento;
            try {
                 dataCancelamento = DateTime.Parse(data);
            }catch(FormatException)
            {
                throw new FormatException("Formato de data invalido!");
            }
           
            Agenda obj = __repository.Find(id);
            if (obj != null) obj.Data = dataCancelamento;
            Validate(obj, (AgendaValidator)Activator.CreateInstance(typeof(AgendaValidator), new object[] { true }));
            obj = __repository.Find(obj.Id);
            base.Delete(obj);
        }
   

        private Agenda JsonToAgenda(string jsonString)
        {
            return UtilsServiice.JsonConvertObject<Agenda>(jsonString); 
        }
    }
}
