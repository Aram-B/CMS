using Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence.Ef;
using Persistence.Ef.Repositories;
using Presentation.Api;
using Services;
using Services.Abstractions;
using System;
using System.IO;
using System.Reflection;
using Web.Middleware;

namespace Web
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
            services.AddControllers()
                .AddApplicationPart(typeof(AssemblyReference).Assembly);

            services.AddRouting(options => options.LowercaseUrls = true);
                        
            services.AddSwaggerGen(c =>
            {                
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Articles Management System",
                    Description = "A simple example of Articles Management System on .NET 5.",                    
                    Contact = new OpenApiContact
                    {
                        Name = "Aram Baghdasaryan",
                        Email = string.Empty                        
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under GNU GPL v3.0",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc(options =>
                options.SuppressAsyncSuffixInActionNames = false
            );

            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddDbContextPool<CmsDbContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("ArticlesDatabase"));
            });

            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Articles Management System v1"));
            }
                                                
            app.UseHttpsRedirection();

            app.UseRouting();
                        
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
                        
        }
    }
}
