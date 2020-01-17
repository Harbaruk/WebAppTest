using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebTestApp.DAL.Infrastructure;
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
                string connStr = Configuration.GetConnectionString(_hostingEnvironment.EnvironmentName);
                if (String.IsNullOrWhiteSpace(connStr))
                {
                    throw new Exception($"No connection string defined for {_hostingEnvironment.EnvironmentName}");
                }
                o.UseSqlServer(connStr);
            }, ServiceLifetime.Scoped);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            
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
