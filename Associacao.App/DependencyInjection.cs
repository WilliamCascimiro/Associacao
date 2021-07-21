using Microsoft.Extensions.DependencyInjection;
using Associacao.Interface.Repositories;
using Associacao.Repository.Repositories;

namespace Associacao.App
{
    public class DependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            RepositoryDependece(serviceCollection);
        }

        private static void RepositoryDependece(IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<IPessoaRepository, PessoaRepository>();
            serviceProvider.AddScoped<IMensalidadeRepository, MensalidadeRepository>();
        }
    }
}
