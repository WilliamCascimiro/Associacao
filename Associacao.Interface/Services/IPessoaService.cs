using Associacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associacao.Interface.Services
{
    public interface IPessoaService
    {
        Task Cadastrar(Pessoa pessoa);
    }
}
