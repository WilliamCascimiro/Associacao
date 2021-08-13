using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Associacao.Domain.Entities;


namespace Associacao.Interface.Repositories
{
    public interface IMensalidadeRepository : IRepository<Mensalidade>
    {
        List<Mensalidade> Get(DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int slcPagamento);
        void Create(Pessoa pessoa, Configuracao configuracao);
        public List<Mensalidade> MensalidadePorPessoa(int idPessoa, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int? slcPagamento);
        List<Mensalidade> MensalidadePorPessoaAnoCorrente(int idPessoa);
        Task<bool> PagarMensalidade(int idMensalidade);
        Task<bool> ReabrirMensalidade(int idMensalidade);

    }
}
