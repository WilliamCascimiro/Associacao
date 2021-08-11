using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Associacao.Domain;
using Associacao.Domain.Entities;
using Associacao.Infra.Data.Context;
using Associacao.Interface.Repositories;
using Associacao.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Associacao.Repository.Repositories
{
    public class ConfiguracaoRepository : IConfiguracaoRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public ConfiguracaoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Alterar(Configuracao configuracao)
        {
            var entity = _dbContext.Configuracoes.Find(configuracao.Id);
            if (entity == null)
                return 0;

            entity.DataCobrancaInicial = configuracao.DataCobrancaInicial;
            entity.DataCobrancaFinal = configuracao.DataCobrancaFinal;
            entity.ValorMensalidade = configuracao.ValorMensalidade;
            entity.DataUltimaAtualizacao = DateTime.Now;

            try
            {
                _dbContext.Configuracoes.Update(entity);
                _dbContext.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Configuracao Get()
        {
            return _dbContext.Configuracoes.OrderBy(x => x.Id).FirstOrDefault();
        }
    }
}
