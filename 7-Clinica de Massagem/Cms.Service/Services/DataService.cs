using Cms.Domain.Entities;
using Cms.Domain.Interfaces;
using Cms.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace Cms.Service.Services
{
   public class DataService : IDataService
    {

        private readonly CmsContext _context;
        private readonly IRepository<Cliente> _repositoryCliente;
        private readonly IRepository<Agenda> _repositoryAgenda;
        public DataService(CmsContext context, IRepository<Cliente> repositoryCliente, IRepository<Agenda> repositoryAgenda)
        {
            _context = context;
            _repositoryAgenda = repositoryAgenda;
            _repositoryCliente = repositoryCliente;
        }


        public void CarregarDados()
        {
            _context.Database.EnsureCreated();

            _repositoryCliente.Incluir(new Cliente(){
                                                Nome= "Amanda ",
                                                Sobrenome = "Queiroz Silva",
                                                CPF = "11111111111",
                                                Endereco = "Rua padre joão cripa",
                                                Cidade = "Campo Grande",
                                                Estado = "MS",
                                                Telefone = "6734123453",
                                                Celular = "67999999999"
                                               });

            _repositoryCliente.Incluir(new Cliente()
                                        {
                                            Nome = "Paulo ",
                                            Sobrenome = "Bezerro Pinto",
                                            CPF = "22211122211",
                                            Endereco = "Rua afonso pena",
                                            Cidade = "Campo Grande",
                                            Estado = "MS",
                                            Telefone = "6734123453",
                                            Celular = "67999999999"
                                        });

            _repositoryCliente.Incluir(new Cliente()
            {
                Nome = "Carla ",
                Sobrenome = "Bianca Silva",
                CPF = "22233322233",
                Endereco = "Rua jose antonio",
                Cidade = "Campo Grande",
                Estado = "MS",
                Telefone = "6734123453",
                Celular = "99999999999"
            });


            DateTime agora = DateTime.Now;


            _repositoryAgenda.Incluir(new Agenda(_repositoryCliente.Find(1), AddDateAndHours(agora, 5,13), 30, ""));
            _repositoryAgenda.Incluir(new Agenda(_repositoryCliente.Find(2), AddDateAndHours(agora, 2, 11), 30, ""));
            if(DateTime.Now.Hour > 8 && DateTime.Now.Hour < 17 )
                _repositoryAgenda.Incluir(new Agenda(_repositoryCliente.Find(3), DateTime.Now, 30, ""));
        }

        private DateTime AddDateAndHours(DateTime date, int days, int hours)
        {
            hours = hours > 23 ? 23 : hours;
            string strHours = hours < 9 ? "0" + hours : hours+"";
            string dateFormat= date.AddDays(days).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)+" "+hours+":00:00";
            return Convert.ToDateTime(dateFormat);
        }

        
    }
}
