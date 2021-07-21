using Associacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Associacao.App.Models
{
    public class PessoaMensalidadeViewModel
    {
        public Pessoa Pessoa;
        public IEnumerable<Mensalidade> Mensalidades;
    }
}
