using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Associacao.Domain;
using Associacao.Domain.Entities;
using Associacao.Infra.Data.Context;
using Associacao.Infra.Data.Repositories;
using Associacao.Interface.Repositories;
using Associacao.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Associacao.Repository.Repositories
{
    public class MensalidadeRepository : Repository<Mensalidade>, IMensalidadeRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        //protected readonly IConfiguracaoRepository _configuracaoRepository;

        public MensalidadeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async void Create(Pessoa pessoa, Configuracao configuracao)
        {
            //var config = await _configuracaoRepository.ObterPorId(1);
            //var config = await _dbContext.Configuracoes.Obter _configuracaoRepository.ObterPorId2(1);            

            List<Mensalidade> mensalidadesList = new();
            var mensalidadeInicial = configuracao.DataCobrancaInicial;
            var mensalidadeFinal = configuracao.DataCobrancaFinal;
            float valorMensalidade = (configuracao.ValorMensalidade * pessoa.QuantidadeCasas);

            while (mensalidadeInicial <= mensalidadeFinal)
            {
                mensalidadeInicial = mensalidadeInicial.AddMonths(1);
                mensalidadesList.Add(new Mensalidade(pessoa.Id, mensalidadeInicial, valorMensalidade));
            }

            _dbContext.Mensalidades.AddRange(mensalidadesList);
            _dbContext.SaveChanges();
        }

        public void Create(int pessoaId, int quantidadeCasas, DateTime mensalidadeInicial, DateTime mensalidadeFinal)
        {
            List<Mensalidade> mensalidadesList = new();
            float valorMensalidade = 10 * quantidadeCasas;

            while (mensalidadeInicial <= mensalidadeFinal)
            {
                mensalidadeInicial = mensalidadeInicial.AddMonths(1);
                mensalidadesList.Add(new Mensalidade(pessoaId, mensalidadeInicial, valorMensalidade));
            }

            _dbContext.Mensalidades.AddRange(mensalidadesList);
            _dbContext.SaveChanges();
        }

        public Mensalidade Detail(int? idMensalidade)
        {
            return _dbContext.Mensalidades
                .Include(m => m.Pessoa)
                .FirstOrDefault(m => m.Id == idMensalidade);
        }

        public List<Mensalidade> MensalidadePorPessoa(int idPessoa, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int? slcPagamento)
        {
            bool? pago = null;
            if (slcPagamento == 1)
                pago = true;
            else if (slcPagamento == 2)
                pago = false;

            var select = _dbContext.Mensalidades.Include(m => m.Pessoa).ToList();

            var result = select
                        .Where(x => x.DataVencimento >= (dataVencimentoInicial == null ? x.DataVencimento : dataVencimentoInicial)
                                 && x.DataVencimento <= (dataVencimentoFinal == null ? x.DataVencimento : dataVencimentoFinal)
                                 && x.Pago == (pago == null ? x.Pago : pago)
                                 && x.IdPessoa == idPessoa)
                        .OrderBy(m => m.DataVencimento)
                        .ToList();
            
            return result;
        }

        public List<Mensalidade> MensalidadePorPessoaAnoCorrente(int idPessoa)
        {
            return _dbContext.Mensalidades
                .Where(m => m.IdPessoa == idPessoa && m.DataVencimento.Year == DateTime.Now.Year)
                .OrderBy(m => m.DataVencimento)
                .ToList();
        }


        public async Task<List<Mensalidade>> Get()
        {
            return await _dbContext.Mensalidades.Include(m => m.Pessoa).ToListAsync();
        }

        public List<Mensalidade> Get(DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int slcPagamento)
        {
            bool? pago = null;
            if (slcPagamento == 1)
                pago = true;
            else if(slcPagamento == 2)
                pago = false;

            var select = _dbContext.Mensalidades.Include(m => m.Pessoa).ToList();
            var result = select
                         .Where(x => x.DataVencimento >= (dataVencimentoInicial == null ? x.DataVencimento : dataVencimentoInicial) 
                                  && x.DataVencimento <= (dataVencimentoFinal == null ? x.DataVencimento : dataVencimentoFinal)
                                  && x.Pago == (pago == null ? x.Pago : pago) )
                         .ToList();

            return result;
        }

        public async Task<bool> PagarMensalidade(int idMensalidade)
        {
            var mensalidade = await ObterPorId(idMensalidade);
            if (mensalidade == null) return false;
            mensalidade.PagarMensalidade();

            try
            {
                _dbContext.Mensalidades.Update(mensalidade);
                _dbContext.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }

        }

        public async Task<bool> ReabrirMensalidade(int idMensalidade)
        {
            var mensalidade = await ObterPorId(idMensalidade);
            if (mensalidade == null) return false;
            mensalidade.ReabrirMensalidade();

            try
            {
                _dbContext.Mensalidades.Update(mensalidade);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
