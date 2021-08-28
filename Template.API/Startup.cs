using Template.API.Configuration;
using Template.API.Core;
using Template.Domain.Queries.Cep;
using Template.Infra;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using FluentValidation.AspNetCore;
using System.Threading.Tasks;
using Template.Application;
using Template.Core.Messages;
using MediatR.Pipeline;
using AutoMapper;

namespace Template.API
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
            // Usa algum handler só pra registrar o assembly
            // Dar um jeito de melhorar isso.
            services.AddMediatorConfiguration(typeof(CepQueryHandler).Assembly);

            // CArrega a classe de configuração do jwt do appsettings.json
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            services.AddHttpContextAccessor();

            // Configurações DI
            services.AddDependencyInjection();

            // Configurações do JWT
            services.AddJwtConfiguration(Configuration);

            // Configurações EF e Identity Core
            //services.AddLivrariaDbContext(Configuration);

            // Fluent Validation
            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(typeof(ApplicationValidations).Assembly);
                });

            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            // Configura o Swagger
            services.AddSwaggerConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria.API v1"));
            }

            
            app.UseCustomSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            // Aplica o JWT
            app.UseJwtConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
