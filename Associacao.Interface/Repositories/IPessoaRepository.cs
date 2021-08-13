using System.Collections.Generic;
using System.Threading.Tasks;
using Associacao.Domain.Entities;

namespace Associacao.Interface.Repositories
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        //Task<Pessoa> Get(int id);
        Task<bool> ExistePendencia(int id);
        Task<Pessoa> Detail(int id);
        bool NumeroCadastroJaExiste(Pessoa pessoa);
        //Task<int> Atualizar(Pessoa pessoa);
        Task Atualizar(Pessoa pessoa);
        Task<List<Pessoa>> GetComplete(string cadastro, string nome, int slcPagamento);
    }
}
