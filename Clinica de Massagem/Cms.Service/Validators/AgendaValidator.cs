using Cms.Domain.Entities;
using Cms.Domain.Interfaces;
using Cms.Service.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Validators
{
    public class AgendaValidator : AbstractValidator<Agenda>
    {
        private void __Validar()
        {
            RuleFor(c => c).NotNull().OnAnyFailure(x =>throw new ArgumentNullException("Argumento invalido"));


            RuleFor(c => c.Data)
                .NotEmpty().WithMessage("É necessario informa a Data")
                .NotNull().WithMessage("É necessario informa a Data");
        }
       
        public AgendaValidator()
        {

            RuleFor(c => c.Cliente)
                .NotEmpty().WithMessage("Cliente é obrigatorio.")
                .NotNull().WithMessage("Cliente é obrigatorio.");

            __Validar();
            RuleFor(c => c.Data).Must(c => !(c.Hour < 8 || c.Hour > 17))
            .WithMessage("Horario de expediente é das 08:00 até as 17:00");
        }


        public AgendaValidator(int somaModalidade)
        {
            RuleFor(a => a.Modalidade).Custom((m, context) => {
                if ((m+somaModalidade)>90)                
                    context.AddFailure($"Esse cliente tem direito apenas uma hora e meia de massagem por mês! Você ja agendou: {UtilsServiice.FormataModalidade(somaModalidade)} ");                
            });
        }

        public AgendaValidator(bool cancelarOuReagendar)
        {
            __Validar();

            RuleFor(c => c.Id)
              .Must(id => !(id.Equals(0))).WithMessage("Id é obrigatorio.");
           

            RuleFor(a => a.Data).Custom((d, context) => {
                if (d.Day==DateTime.Now.Day)
                    context.AddFailure("Esse cliente pode cancelar ou reagendar uma massagem com até um dia de antecedência!");                
            });
        }
    }
}