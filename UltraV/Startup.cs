using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.DataAccess;
using Domain.Services;
using Domain.Services.Implementation;
using Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace UltraV
{
    public class Startup
    {
        private readonly ServicesSettings servicesSettings;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.Configuration.GetSection("Services").Bind(this.servicesSettings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(
                ctx =>
                {
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(this.Configuration.GetConnectionString("Connection"))
                        .Options;

                    return new DataContext(options);
                });

            services.AddSingleton<GenerateToken>();
            services.AddTransient<ICharacterService, CharacterService>();

            services.AddSingleton(
                typeof(ServicesSettings),
                options =>
                {
                    var service = new ServicesSettings();
                    this.Configuration.GetSection("Services").Bind(service);

                    return service;
                });
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "Ultra API", Version = "v1"}); });
            services.AddAutoMapper();
            services.AddHttpClient();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ultra API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}