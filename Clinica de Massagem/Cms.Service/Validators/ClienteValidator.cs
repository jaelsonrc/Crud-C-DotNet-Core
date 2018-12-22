using Cms.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Validators
{
   public class ClienteValidator : AbstractValidator<Cliente>
    {


        public ClienteValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Objeto cliente não encontrado");
                });

            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("CPF é obrigatorio.")
                .NotNull().WithMessage("CPF é obrigatorio.");
          

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessario informa o nome")
                .NotNull().WithMessage("É necessario informa o nome");
        }

        public ClienteValidator(bool podeExcluir)
        {
            RuleFor(a => a.Agendas).Custom((d, context) => {
                if (d != null && d.Count > 0)
                    context.AddFailure("Você não pode excluir cliente que possui agendamento!");
            });
        }


        }


}
