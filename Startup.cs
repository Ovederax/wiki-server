using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using wiki_server.Models;

namespace wiki_server
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddRazorViewEngine();
            //string s = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;
            //string s = Configuration.GetConnectionString("ConnectionStrings");
            string s = "server=127.0.0.1; port=3306; database=wikidb; user=root; password=test;";
            services.AddControllers();
            services.Add(
                new ServiceDescriptor(typeof(DatabaseContext), 
                new DatabaseContext(s)));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(builder => builder
                                        .AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

        }
    }
}
