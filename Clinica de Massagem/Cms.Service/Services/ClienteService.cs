using Cms.Domain.Entities;
using Cms.Domain.Interfaces;
using Cms.Infrastructure.Repositorys;
using Cms.Service.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Services
{
   public class ClienteService : BaseService<Cliente>, IService<Cliente>
    {
        public ClienteService(IRepository<Cliente> repository) : base(repository)
        {
        }


        public override void Delete(Cliente obj)
        {
            BaseRepository<Cliente> repo = __repository as BaseRepository<Cliente>;
            DbSet<Cliente> query = repo.DbSet as DbSet<Cliente>;

           Cliente clienteVerifica = query.Include(c => c.Agendas).Where(x => x.Id==obj.Id).SingleOrDefault();

            Validate(clienteVerifica, (ClienteValidator)Activator.CreateInstance(typeof(ClienteValidator), new object[] { true }));

            base.Delete(obj);
        }
    }
}
