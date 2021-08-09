using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Config.Demo
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

            services.AddControllers();
            var version = Configuration.GetValue<string>("version");
            //var db = Configuration["Settings:Databse"];
            Console.WriteLine(version);
           // Console.WriteLine(db);
            services.AddSingleton(version);
            // services.AddSingleton(db);

            //Adding Through Dependency Injection
            services.Configure<Custom>(Configuration.GetSection("Custom"));

            //Adding Configuration Through Signleton
            var Custom = new Custom();
            Configuration.GetSection("Custom").Bind(Custom);
            services.AddSingleton(Custom);

            //Get Configuration from Property Window
            Console.WriteLine(Configuration["ASPNETCORE_ENVIRONMENT"]);
            //get settings from Custom Config Json
            Console.WriteLine(Configuration["CustomConfig"]);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Config.Demo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Config.Demo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
