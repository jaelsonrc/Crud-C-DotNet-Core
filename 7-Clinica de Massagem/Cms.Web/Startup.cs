using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Cms.Domain.Interfaces;
using Cms.Domain.Entities;
using Cms.Infrastructure.Repositorys;
using Cms.Infrastructure.Context;
using Cms.Service.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Cms.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {   
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
          
            services.AddTransient<IRepository<Cliente>, BaseRepository<Cliente>>();
            services.AddTransient<IRepository<Agenda>, BaseRepository<Agenda>>();
            services.AddTransient<IService<Cliente>, ClienteService>();
            services.AddTransient<IAgendaService, AgendaService>();
            services.AddTransient<IDataService, DataService>();

            string dbName = Guid.NewGuid().ToString();
            services.AddDbContext<CmsContext>(options => options.UseInMemoryDatabase(dbName));
            //services.AddDbContext<CmsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CmsDb")));
                services.AddMvc(options =>
                {
                    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)               
                 .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                 }); 
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
               IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

           

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();

            serviceProvider.GetService<IDataService>().CarregarDados();
        }
    }
}
