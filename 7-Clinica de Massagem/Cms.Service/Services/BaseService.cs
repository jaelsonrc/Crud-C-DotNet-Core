using Cms.Domain.Interfaces;
using Cms.Domain.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Services
{
   public class BaseService<T> : IService<T> where T : BaseEntity
    {

        protected IRepository<T> __repository;

        public BaseService(IRepository<T> repository)
        {
            __repository = repository;
        }
       

        public virtual T Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());
            __repository.Incluir(obj);
            return obj;
        }

        public virtual T Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            __repository.Alterar(obj);
            return obj;
        }

        public virtual void Delete(T obj)
        {
            if (obj.Id == 0)
                throw new ArgumentException("O Id não pode ser zero");

            __repository.Excluir(obj);
        }

        public virtual IList<T> Get() => __repository.Get.ToList();

        public virtual T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("O Id não pode ser zero");

            return __repository.Find(id);
        }

        protected void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }


    }
}
