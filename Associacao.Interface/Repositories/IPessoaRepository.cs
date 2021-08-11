using System.Collections.Generic;
using System.Threading.Tasks;
using Associacao.Domain.Entities;

namespace Associacao.Interface.Repositories
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<List<Pessoa>> Get();
        Task<Pessoa> Get(int id);
        Task<bool> ExistePendencia(int id);
        
        List<Pessoa> Search(string text);
        Task<Pessoa> Detail(int id);
        void Create(Pessoa pessoa);
        bool NumeroCadastroDisponivel(Pessoa pessoa);
        int Alterar(Pessoa pessoa);
        Task<List<Pessoa>> GetComplete();
        Task<List<Pessoa>> GetComplete(string cadastro, string nome, int slcPagamento);
        void GetComplete2();
    }
}
