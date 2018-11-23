using System;
using System.IO;
using System.Reflection;
using Api.NetCore.DataAccess;
using Api.NetCore.Hubs;
using Api.NetCore.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Api.NetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly("Api.NetCore.DataAccess")));

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyOrigin();
            }));
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        
            services.AddSignalR();
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();
            var authority = Configuration.GetSection("Authority").Value;
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authority;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "api1";
                });
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Api.NetCore", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ApplicationContainer = AutoFacConfig.Configure(services);

            return new AutofacServiceProvider(ApplicationContainer);
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
            app.UseAuthentication();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.NetCore V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/hub");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
