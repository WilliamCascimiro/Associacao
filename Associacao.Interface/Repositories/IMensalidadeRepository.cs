using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Associacao.Domain.Entities;


namespace Associacao.Interface.Repositories
{
    public interface IMensalidadeRepository
    {
        Task<List<Mensalidade>> Get();
        List<Mensalidade> Get(DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int slcPagamento);        
        List<Mensalidade> Search(int idPessoa);
        List<Mensalidade> SearchByVencimento(DateTime dataVencimento);
        List<Mensalidade> SearchByPagamento(DateTime dataPagamento);
        Mensalidade Detail(int idMensalidade);
        void Create(int pessoaId, int quantidadeCasas, DateTime mensalidadeInicial, DateTime mensalidadeFinal);
        void Create(int pessoaId, int quantidadeCasas);
        bool PagarMensalidade(int idMensalidade);
        bool ReabrirMensalidade(int idMensalidade);
        bool ExistePendencia(int idMensalidade);
        public List<Mensalidade> MensalidadePorPessoa(int idPessoa);
        public List<Mensalidade> MensalidadePorPessoa(int idPessoa, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int? slcPagamento);
        List<Mensalidade> MensalidadePorPessoaAnoCorrente(int idPessoa);

    }
}
