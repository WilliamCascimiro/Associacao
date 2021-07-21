using Microsoft.Extensions.DependencyInjection;
using Associacao.Interface.Repositories;
using Associacao.Repository.Repositories;



namespace Associacao.Web
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
        }
    }
}
