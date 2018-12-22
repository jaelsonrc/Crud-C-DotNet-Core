using Cms.Domain.Entities;
using Cms.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;




namespace Cms.Infrastructure.Context
{
    public class CmsContext : DbContext
    {

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        

        public CmsContext(DbContextOptions<CmsContext> options)
            : base(options)
        {
            //irá criar o banco e a estrutura de tabelas necessárias
           this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         //   optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Cliente>(new ClienteMap());
        }

    }
}
