using Associacao.Infra.Data.Context;
using Associacao.Interface.Repositories;
using Associacao.Interface.Services;
using Associacao.Repository.Context;
using Associacao.Repository.Repositories;
using Associacao.Service.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Associacao.App.Configuration
{
    public static class DIConfiguration
    {
        public static IServiceCollection Config(this IServiceCollection services, IConfiguration configuration)
        {
            services = AddDependencyInjection(services);
            services = AddDIService(services);
            services = AddContext(services, configuration);
            services = AddAutoMapper(services);

            return services;
        }

        public static IServiceCollection AddContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseNpgsql(
                    configuration.GetConnectionString("App"),
                    assembly => assembly.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            return services;
        }

        public static IServiceCollection AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            return services;
        }

        public static IServiceCollection AddDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IMensalidadeRepository, MensalidadeRepository>();
            services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository>();

            return services;
        }

        public static IServiceCollection AddDIService(IServiceCollection services)
        {
            services.AddScoped<IPessoaService, PessoaService>();

            return services;
        }

    }
}
