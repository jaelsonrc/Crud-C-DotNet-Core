using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cms.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {       
        IEnumerable<TEntity> Get { get; }
        TEntity Find(int key);
        TEntity Find(int key, bool Detached);
        void Incluir(params TEntity[] obj);
        void Alterar(params TEntity[] obj);
        void Excluir(params TEntity[] obj);
    }
}
