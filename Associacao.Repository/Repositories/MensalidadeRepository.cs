﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Associacao.Domain;
using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Associacao.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace Associacao.Repository.Repositories
{
    public class MensalidadeRepository : IMensalidadeRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public MensalidadeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(int pessoaId, int quantidadeCasas)
        {
            var mensalidadesList = new List<Mensalidade>();
            //var mensalidadeInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var mensalidadeInicial = new DateTime(2020, 12, 30);
            var mensalidadeFinal = new DateTime(2022, 12, 30);
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

        public List<Mensalidade> PorPessoa(int idPessoa)
        {
            return _dbContext.Mensalidades
                .Where(m => m.IdPessoa == idPessoa)
                .OrderBy(m => m.DataVencimento)
                .ToList();
        }

        public List<Mensalidade> MensalidadePorPessoaAnoCorrente(int idPessoa)
        {
            return _dbContext.Mensalidades
                .Where(m => m.IdPessoa == idPessoa && m.DataVencimento.Year == DateTime.Now.Year)
                .OrderBy(m => m.DataVencimento)
                .ToList();
        }


        public List<Mensalidade> Get()
        {
            return _dbContext.Mensalidades.ToList();
        }

        public List<Mensalidade> Search(int idPessoa)
        {
            throw new NotImplementedException();
        }

        public List<Mensalidade> SearchByPagamento(DateTime dataPagamento)
        {
            throw new NotImplementedException();
        }

        public List<Mensalidade> SearchByVencimento(DateTime dataVencimento)
        {
            throw new NotImplementedException();
        }

        public Mensalidade Detail(int idMensalidade)
        {
            throw new NotImplementedException();
        }

        public bool PagarMensalidade(int idMensalidade)
        {
            Mensalidade mensalidade = _dbContext.Mensalidades.Find(idMensalidade);
            if (mensalidade == null)
                return false;

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

        public Mensalidade RecuperaMensalidade(int idMensalidade)
        {
            return _dbContext.Mensalidades.Find(idMensalidade);
        }

        public bool ReabrirMensalidade(int idMensalidade)
        {
            var mensalidade = RecuperaMensalidade(idMensalidade);
            if (mensalidade == null)
                return false;

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

        public bool ExistePendencia(int idPessoa)
        {
            return _dbContext.Mensalidades.Any(m => m.DataVencimento < DateTime.Now && !m.Pago);
        }
    }
}
