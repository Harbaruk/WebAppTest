using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebTestApp.DAL.Infrastructure;
using WebTestApp.Services.FileReaders;
using WebTestApp.Services.MappingConfigurations;
using WebTestApp.Services.Transactions;

namespace WebTestApp.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppTest;Trusted_Connection=True;MultipleActiveResultSets=true");
            }, ServiceLifetime.Scoped);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IFileProcessorFactory, FileProcessorFactory>();
            services.AddTransient<ITransactionService, TransactionService>();

            services.AddLogging(config =>
            {
                config.AddConsole();
            });

            services.AddAutoMapper(typeof(ServicesMappingConfig));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
        }
    }
}
