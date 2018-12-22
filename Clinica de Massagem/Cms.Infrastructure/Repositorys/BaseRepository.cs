using System.Collections.Generic;
using System.Linq;
using Cms.Domain.Interfaces;
using Cms.Domain.Shared;
using Cms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Repositorys
{
   public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CmsContext context;
        public DbSet<TEntity> DbSet { get; }

        public BaseRepository(CmsContext context)
        {
            this.context = context;
            DbSet = this.context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get => DbSet.AsQueryable();

        public void Alterar(params TEntity[] obj)
        {
            DbSet.UpdateRange(obj);
            context.SaveChanges();
        }

        public void Excluir(params TEntity[] obj)
        {
            DbSet.RemoveRange(obj);
            context.SaveChanges();
        }

       

        public void Incluir(params TEntity[] obj)
        {
            DbSet.AddRange(obj);
            context.SaveChanges();
        }

        public TEntity Find(int key)
        {
            return DbSet.Find(key);
        }

        public TEntity Find(int key, bool isDetached)
        {
            TEntity entity = Find(key);
            if(entity != null && isDetached)  context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
