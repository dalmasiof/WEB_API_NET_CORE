using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection;
using System.IO;

namespace SmartSchool.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            //services.AddSingleton<IRepository,Repository>();
            //Cria uma instancia para cada requisição e a reutiliza em varios locais podendo trazer varios problemas

            //services.AddTransient<IRepository,Repository>();
            //Sempre criando novas instancia e nunca reutilizando

            services.AddScoped<IRepository, Repository>();
            //Reutiliza instancia caso haja dependencia, caso contrario cria outra. Usado prlo prof, recomendadado

                services.AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc
                ("smartschoolapi", new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SmarSchool API",
                        Version = "1.0"
                    });

                }

            );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



            services.AddControllers()
            .AddNewtonsoftJson(op =>
            op.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(
                opt => opt.SwaggerEndpoint("/swagger/smartschoolapi/swagger.json",
                "smartschoolapi")
            );
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
