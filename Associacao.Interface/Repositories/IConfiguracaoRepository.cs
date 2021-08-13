using System.Collections.Generic;
using System.Threading.Tasks;
using Associacao.Domain.Entities;

namespace Associacao.Interface.Repositories
{
    public interface IConfiguracaoRepository : IRepository<Configuracao>
    {
        Configuracao Get();
        Task<Configuracao> ObterPorId2(int id);
    }
}
