using System.Collections.Generic;
using System.Threading.Tasks;
using Associacao.Domain.Entities;

namespace Associacao.Interface.Repositories
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<bool> ExistePendencia(int id);
        bool NumeroCadastroJaExiste(Pessoa pessoa);
        Task<List<Pessoa>> GetComplete(string cadastro, string nome, int slcPagamento);
    }
}
