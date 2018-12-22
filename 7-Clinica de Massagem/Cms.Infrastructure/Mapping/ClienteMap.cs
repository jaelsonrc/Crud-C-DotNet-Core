using Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Mapping
{
    class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder
               .Property(T => T.Nome)
               .HasColumnType("nvarchar(100)")
               .IsRequired();

            builder
                .Property(T => T.CPF)
                .HasColumnType("nvarchar(14)")
                .IsRequired();

            builder
               .Property(T => T.Celular)
               .HasColumnType("nvarchar(16)")
               .IsRequired();

            builder
               .Property(T => T.Endereco)
               .IsRequired();

        }
    }
}
