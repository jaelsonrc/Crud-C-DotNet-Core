using Cms.Domain.Entities;
using Cms.Domain.Interfaces;
using NUnit.Framework;
using Moq;
using Cms.Tests;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Cms.Service.Services;
using Cms.Service.Validators;

namespace Tests
{
    public class TestAgenda
    {
        private Mock<IRepository<Cliente>> mockRepositoryCliente;
        private Mock<IRepository<Agenda>> mockRepositoryAgenda;
        private Mock<DbSet<Agenda>> mockDbSetAgenda;  
        private IQueryable<Agenda> queryAgendas;
        private IQueryable<Cliente> queryClientes;
        private IRepository<Cliente> repositoryCliente;


        public TestAgenda()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(new Cliente()
            {
                Id = 1,
                Nome = "Paulo ",
                Sobrenome = "Bezerro Pinto",
                CPF = "222.111.222-11",
                Endereco = "Rua afonso pena",
                Cidade = "Campo Grande",
                Estado = "MS",
                Telefone = "67-3412-3453",
                Celular = "67-99999-9999"
            });
            queryClientes = clientes.AsQueryable();
            queryAgendas = new List<Agenda>().AsQueryable();
            mockRepositoryAgenda = new Mock<IRepository<Agenda>>();
            mockDbSetAgenda = new Mock<DbSet<Agenda>>();
            mockRepositoryCliente = new Mock<IRepository<Cliente>>();


        }


        [SetUp]
        public void Setup()
        {
          
            mockRepositoryCliente.Setup(m => m.Find(It.IsAny<int>())).Returns(queryClientes.FirstOrDefault);

            repositoryCliente = mockRepositoryCliente.Object;                     
        }
 

        [Test]
        public void DeveLancaExceptionDaModalidadeExcedida()
        {

          
            var lista = new List<Agenda>();
            lista.Add(new Agenda()
            {
                Id = 1,
                Cliente = repositoryCliente.Find(1),
                Data = UtilsTest.AddDateAndHours(DateTime.Now, 7, 13),
                Modalidade = 60,
                Obs = ""
            });

            lista.Add(new Agenda()
            {
                Id = 3,
                Cliente = repositoryCliente.Find(1),
                Data = UtilsTest.AddDateAndHours(DateTime.Now, 8, 14),
                Modalidade = 30,
                Obs = ""
            });
            queryAgendas = lista.AsQueryable();
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.Provider).Returns(queryAgendas.Provider);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.Expression).Returns(queryAgendas.Expression);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.ElementType).Returns(queryAgendas.ElementType);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.GetEnumerator()).Returns(queryAgendas.GetEnumerator());

            mockRepositoryAgenda.Setup(m => m.Get).Returns(mockDbSetAgenda.Object);



            Agenda agendaPost = new Agenda()
            {
                Id = 1,
                Cliente = repositoryCliente.Find(1),
                Data = UtilsTest.AddDateAndHours(DateTime.Now, 5, 15),
                Modalidade = 30,
                Obs = ""
            };

     
            AgendaService service = new AgendaService(mockRepositoryAgenda.Object, repositoryCliente);

            Assert.That(() => service.Post<AgendaValidator>(agendaPost), Throws.TypeOf<FluentValidation.ValidationException>());

        }


        [Test]
        public void DeveLancaExceptionDaTempoForaDoHorarioExpediente()
        {


            var lista = new List<Agenda>();     
            queryAgendas = lista.AsQueryable();
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.Provider).Returns(queryAgendas.Provider);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.Expression).Returns(queryAgendas.Expression);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.ElementType).Returns(queryAgendas.ElementType);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.GetEnumerator()).Returns(queryAgendas.GetEnumerator());

          
            mockRepositoryAgenda.Setup(m => m.Get).Returns(mockDbSetAgenda.Object);



            Agenda agendaPost = new Agenda()
            {
                Id = 1,
                Cliente = repositoryCliente.Find(1),
                Data = UtilsTest.AddDateAndHours(DateTime.Now, 5, 19),
                Modalidade = 30,
                Obs = ""
            };


            AgendaService service = new AgendaService(mockRepositoryAgenda.Object, repositoryCliente);

            Assert.That(() => service.Post<AgendaValidator>(agendaPost), Throws.TypeOf<FluentValidation.ValidationException>());

        }


        [Test]
        public void DeveLancaExceptionDoPrazoParaAlteracaoDoAgendamento()
        {


            var lista = new List<Agenda>();
            lista.Add(new Agenda()
            {
                Id = 1,
                Cliente = repositoryCliente.Find(1),
                Data = DateTime.Now,
                Modalidade = 60,
                Obs = ""
            });

         
            queryAgendas = lista.AsQueryable();
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.Provider).Returns(queryAgendas.Provider);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.Expression).Returns(queryAgendas.Expression);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.ElementType).Returns(queryAgendas.ElementType);
            mockDbSetAgenda.As<IQueryable<Agenda>>().Setup(m => m.GetEnumerator()).Returns(queryAgendas.GetEnumerator());

            mockRepositoryAgenda.Setup(m => m.Get).Returns(mockDbSetAgenda.Object);
            mockRepositoryAgenda.Setup(m => m.Find(It.IsAny<int>(),true)).Returns(queryAgendas.FirstOrDefault);



            Agenda agendaPut = new Agenda()
            {
                Id = 1,
                Cliente = repositoryCliente.Find(1),
                Data = UtilsTest.AddDateAndHours(DateTime.Now, 5, 15),
                Modalidade = 30,
                Obs = ""
            };


            AgendaService service = new AgendaService(mockRepositoryAgenda.Object, repositoryCliente);

            Assert.That(() => service.Put<AgendaValidator>(agendaPut), Throws.TypeOf<FluentValidation.ValidationException>());

        }

    }
}