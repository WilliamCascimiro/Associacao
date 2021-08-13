using Associacao.Domain.Entities;
using System.Threading.Tasks;

namespace Associacao.Interface.Services
{
    public interface IMensalidadeService
    {
        Task Cadastrar(Mensalidade pessoa);
    }
}
