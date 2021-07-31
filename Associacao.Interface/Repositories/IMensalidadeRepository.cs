using System;
using System.Collections.Generic;
using Associacao.Domain.Entities;


namespace Associacao.Interface.Repositories
{
    public interface IMensalidadeRepository
    {
        List<Mensalidade> GetAll();
        List<Mensalidade> GetAll(DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int slcPagamento);        
        List<Mensalidade> Search(int idPessoa);
        List<Mensalidade> SearchByVencimento(DateTime dataVencimento);
        List<Mensalidade> SearchByPagamento(DateTime dataPagamento);
        Mensalidade Detail(int idMensalidade);
        void Create(int pessoaId, int quantidadeCasas, DateTime mensalidadeInicial, DateTime mensalidadeFinal);
        void Create(int pessoaId, int quantidadeCasas);
        bool PagarMensalidade(int idMensalidade);
        bool ReabrirMensalidade(int idMensalidade);
        bool ExistePendencia(int idMensalidade);
        List<Mensalidade> PorPessoa(int idPessoa);
        List<Mensalidade> MensalidadePorPessoaAnoCorrente(int idPessoa);

    }
}
