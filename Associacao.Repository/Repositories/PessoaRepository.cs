using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Associacao.Domain.Entities;
using Associacao.Infra.Data.Context;
using Associacao.Infra.Data.Repositories;
using Associacao.Interface.Repositories;
using Associacao.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Associacao.Repository.Repositories
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        protected readonly ApplicationDbContext _context;

        public PessoaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pessoa> Detail(int id)
        {
            return await _context.Pessoas
                    .Include(x => x.Mensalidades)
                    .FirstOrDefaultAsync(x => x.Ativo && x.Id == id);
        }

        //public Pessoa GetWithCurrentYear(int id)
        //{
        //    return _context.Pessoas
        //            .Include(x => x.Mensalidades.Where(m => m.DataVencimento.Year == DateTime.Now.Year).OrderBy(m => m.DataVencimento))
        //            .FirstOrDefault(x => x.Ativo && x.Id == id);
        //}

        public async Task<Pessoa> Get(int id)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(x => x.Ativo && x.Id == id);
        }

        public async Task<bool> ExistePendencia(int id)
        {
            return await _context.Mensalidades.AnyAsync(m => m.DataVencimento < DateTime.Now && !m.Pago);
        }

        public async Task<List<Pessoa>> Get()
        {
            return await _context.Pessoas
                .Where(p => p.Ativo)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<List<Pessoa>> GetComplete(string cadastro, string nome, int slcPagamento)
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

            var result = _context.Pessoas
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

            result = result.Where(x =>  x.NumeroCadastro.Contains(cadastro) && 
                                        x.Nome.ToUpper().Contains(nome.ToUpper()) && 
                                        x.Adimplente == (pag == null ? x.Adimplente : pag))
                           .ToList();

            return result;
        }

        public async Task<List<Pessoa>> GetComplete()
        {
            var teste = _context.Pessoas
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
                .ToListAsync();

            return await teste;
        }

        public void GetComplete2()
        {
            object ret = _context.Pessoas
                .Include(x => x.Mensalidades.Where(m => m.DataVencimento < DateTime.Now && m.Pago == false))
                .Select(s => new Pessoa
                {
                    Nome = s.Nome,
                    Logradouro = s.Logradouro,
                    Adimplente = s.Mensalidades.Count() >= 1 ? false : true
                }).ToList();
        }

        //.FirstOrDefault();

        //var query2 = _context.Pessoas
        //    .Join(
        //        _context.Mensalidades,
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


        //var query2 = _context.Pessoas
        //    .GroupJoin(
        //        _context.Mensalidades,
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

        //var qq = _context.Pessoas(p => p.Nome, p.Mensalidades } );

        //var query = from p in _context.Pessoas
        //            join m in _context.Mensalidades
        //                on p.Id equals m.IdPessoa into grouping
        //            select new { p, Menalidades = grouping.Where(p => p.Content.Contains("EF")).ToList() };


        //var query2 = _context.Pessoas
        //    .GroupJoin(
        //        _context.Mensalidades,
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
            return _context.Pessoas
                .Where(p => p.Ativo && (p.Nome.ToUpper().Contains(text.ToUpper())))
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public void Create(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
        }

        public bool NumeroCadastroDisponivel(Pessoa pessoa)
        {
            //var numerocadastro = _context.Pessoas.Find(pessoa.NumeroCadastro);
            var numerocadastro = _context.Pessoas.Where(p => p.NumeroCadastro == pessoa.Numero).FirstOrDefault();
            if (numerocadastro != null)
                return false;

            return true;
        }

        public int Alterar(Pessoa pessoa)
        {
            var entity = _context.Pessoas.Find(pessoa.Id);
            if (entity == null)
                return 0;

            entity.Nome = pessoa.Nome;
            entity.RG = pessoa.RG;

            try
            {
                _context.Pessoas.Update(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        
    }
}
