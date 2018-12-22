using System.Collections.Generic;
using Cms.Domain.Entities;
using FluentValidation;

namespace Cms.Service.Services
{
    public interface IAgendaService
    {
        void Delete(int id, string data);
        IList<Agenda> Get();
        Agenda Get(int id);
        Agenda Post(string jsonString);
        Agenda Post<V>(Agenda obj) where V : AbstractValidator<Agenda>;
        Agenda Put(string jsonString);
        Agenda Put<V>(Agenda obj) where V : AbstractValidator<Agenda>;
    }
}