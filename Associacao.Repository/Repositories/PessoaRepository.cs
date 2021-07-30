using System;
using System.Collections.Generic;
using System.Linq;
using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Associacao.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Associacao.Repository.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public PessoaRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Pessoa Detail(int id)
        {
            return  _dbContext.Pessoas
                    .Include(x => x.Mensalidades)
                    .Where(x => x.Ativo && x.Id == id)
                    .FirstOrDefault();
        }

        public List<Pessoa> Get()
        {
            return _dbContext.Pessoas
                .Where(p => p.Ativo)
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public List<Pessoa> GetComplete(string cadastro, string nome, int slcPagamento)
        {
            if (String.IsNullOrEmpty(cadastro))
                cadastro = "";

            if (String.IsNullOrEmpty(nome))
                nome = "";

            bool? pag = null;

            if (slcPagamento == 1)
                pag = true;
            else if(slcPagamento == 2)
                pag = false;

            var result = _dbContext.Pessoas
                .Include(x => x.Mensalidades)
                .Select(s => new Pessoa
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Logradouro = s.Logradouro,
                    Numero = s.Numero,
                    Complemento = s.Complemento,
                    Telefone1 = "(" + s.Telefone1.Substring(0, 2) + ") " + s.Telefone1.Substring(2, 5) + "-" + s.Telefone1.Substring(7, 4),
                    NumeroCadastro = s.NumeroCadastro,
                    QuantidadeCasas = s.QuantidadeCasas,
                    Adimplente = s.Mensalidades.Where(m => m.DataVencimento < DateTime.Now && m.Pago == false).Count() >= 1 ? false : true
                })
                .ToList();

            result = result.Where(x => x.NumeroCadastro.Contains(cadastro) && 
                                        x.Nome.Contains(nome) && 
                                        x.Adimplente == (pag == null ? x.Adimplente : pag)
                                       ).ToList();

            return result;
        }

        public List<Pessoa> GetComplete()
        {
            var teste = _dbContext.Pessoas
                .Include(x => x.Mensalidades)
                .Select(s => new Pessoa
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Logradouro = s.Logradouro,
                    Numero = s.Numero,
                    Complemento = s.Complemento,
                    Telefone1 = "(" + s.Telefone1.Substring(0, 2) + ") " + s.Telefone1.Substring(2, 5) + "-" + s.Telefone1.Substring(7, 4),
                    NumeroCadastro = s.NumeroCadastro,
                    QuantidadeCasas = s.QuantidadeCasas,
                    Adimplente = s.Mensalidades.Where(m => m.DataVencimento < DateTime.Now && m.Pago == false).Count() >= 1 ? false : true
                })
                .ToList();

            return teste;
        }

        public void GetComplete2()
        {
            object ret = _dbContext.Pessoas
                .Include(x => x.Mensalidades.Where(m => m.DataVencimento < DateTime.Now && m.Pago == false))
                .Select(s => new Pessoa
                {
                    Nome = s.Nome,
                    Logradouro = s.Logradouro,
                    Adimplente = s.Mensalidades.Count() >= 1 ? false : true
                }).ToList();
        }

        //.FirstOrDefault();

        //var query2 = _dbContext.Pessoas
        //    .Join(
        //        _dbContext.Mensalidades,
        //        pessoa => pessoa.Id,
        //        mensalidade => mensalidade.IdPessoa,
        //        (pessoa, mensalidade) => new
        //        {
        //            PessoaId = pessoa.Id,
        //            //Nome = pessoa.Nome,
        //            //Cep = pessoa.Cep,
        //            Pago = pessoa.Mensalidades.Where(m => m.DataVencimento >= DateTime.Now && m.Pago == false).Count(),
        //            //DataVencimento = mensalidade.DataVencimento
        //        }
        //    ).GroupBy(c => c.PessoaId);
        //.ToList();


        //var query2 = _dbContext.Pessoas
        //    .GroupJoin(
        //        _dbContext.Mensalidades,
        //        pessoa => pessoa.Id,
        //        mensalidade => mensalidade.IdPessoa,
        //        (pessoa, mensalidade) => new { Pessoa = pessoa, Mensalidade = mensalidade.DefaultIfEmpty() })
        //    .SelectMany(final => final.Mensalidade,
        //                (final, mensalidade) => new
        //                {
        //                    IdUser = final.Pessoa.Id,
        //                    FirstName = final.Pessoa.Nome
        //                    // Uma forma de proteger do null, não testado.
        //                    //IdExam = ex != null ? ex.Id : 0
        //                })
        //    //.Where(final => (final.Ex == null) ||
        //    //        ((final.Ex.Id == null || final.Ex.InitialDate == null || final.Ex.InitialDate >= DateTime.Now) &&
        //    //        (final.Ex.EndDate == null || final.Ex <= DateTime.Now)))
        //    .ToList();

        //var qq = _dbContext.Pessoas(p => p.Nome, p.Mensalidades } );

        //var query = from p in _dbContext.Pessoas
        //            join m in _dbContext.Mensalidades
        //                on p.Id equals m.IdPessoa into grouping
        //            select new { p, Menalidades = grouping.Where(p => p.Content.Contains("EF")).ToList() };


        //var query2 = _dbContext.Pessoas
        //    .GroupJoin(
        //        _dbContext.Mensalidades,
        //        pessoa => pessoa.Id,
        //        mensalidade => mensalidade.IdPessoa,
        //        (pessoa, mensalidade) => new
        //        {
        //            PessoaId = pessoa.Id,
        //            Nome = pessoa.Nome,
        //            Cep = pessoa.Cep,
        //            TEste = mensalidade.DefaultIfEmpty()
        //            //Pago = pessoa.Mensalidades.Where(p => p.Pago),// mensalidade.Pago,
        //            //DataVencimento = mensalidade.DataVencimento
        //        }
        //    ).ToList();

        //var pessoa = new Pessoa()




        //foreach (var result in query) 
        //{
        //    result.Cep
        //}

        //.Where(p => p.Ativo)
        //.OrderBy(p => p.Nome)
        //.ToList();

        public List<Pessoa> Search(string text)
        {
            return _dbContext.Pessoas
                .Where(p => p.Ativo && (p.Nome.ToUpper().Contains(text.ToUpper())))
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public void Create(Pessoa pessoa)
        {
            _dbContext.Pessoas.Add(pessoa);
            _dbContext.SaveChanges();
        }

        public int Alterar(Pessoa pessoa)
        {
            var entity = _dbContext.Pessoas.Find(pessoa.Id);
            if (entity == null)
                return 0;

            entity.Nome = pessoa.Nome;
            entity.RG = pessoa.RG;

            try
            {
                _dbContext.Pessoas.Update(entity);
                _dbContext.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
