using System.Collections.Generic;
using Associacao.Domain.Entities;

namespace Associacao.Interface.Repositories
{
    public interface IConfiguracaoRepository
    {
        Configuracao Get();
        int Alterar(Configuracao configuracao);
    }
}
