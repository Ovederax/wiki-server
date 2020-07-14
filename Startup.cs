using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using wiki_server.Models;
using wiki_server.Services;

namespace wiki_server
{
    public class Startup
    {

        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment CurrentEnvironment { get; set; }
        public IConfigurationRoot Configuration { get; }
        public Startup(IWebHostEnvironment env)
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
            string connectStr = Configuration["ConnectionStrings"];
            services.AddControllers();
            services.AddSingleton(new DatabaseContext(connectStr));
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
