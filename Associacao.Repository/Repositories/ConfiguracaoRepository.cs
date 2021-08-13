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
    public class ConfiguracaoRepository : Repository<Configuracao>, IConfiguracaoRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public ConfiguracaoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async override Task Atualizar(Configuracao configuracao)
        {
            var entity = await ObterPorId(configuracao.Id);

            entity.DataCobrancaInicial = configuracao.DataCobrancaInicial;
            entity.DataCobrancaFinal = configuracao.DataCobrancaFinal;
            entity.ValorMensalidade = configuracao.ValorMensalidade;
            entity.DataUltimaAtualizacao = DateTime.Now;

            _dbContext.Configuracoes.Update(entity);
            _dbContext.SaveChanges();
        }

        public override async Task<Configuracao> ObterPorId(int id)
        {
            return await _context.Configuracoes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Configuracao> ObterPorId2(int id)
        {
            return await _context.Configuracoes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Configuracao Get()
        {
            return _dbContext.Configuracoes.OrderBy(x => x.Id).FirstOrDefault();
        }
    }
}
