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

        public override async Task<Pessoa> ObterPorId(int id)
        {
            return await _context.Pessoas
                .Include(x => x.Mensalidades)
                .FirstOrDefaultAsync(x => x.Ativo && x.Id == id);
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

            var select = _context.Pessoas
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
                                Adimplente = s.Mensalidades.Where(m => m.DataVencimento < DateTime.Now && m.Pago == false).Count() >= 1 ? false : true,
                                Ativo = s.Ativo
                            })
                            .ToList();

            select =  select.Where(x =>  x.NumeroCadastro.Contains(cadastro) &&
                                        x.Nome.ToUpper().Contains(nome.ToUpper()) &&
                                        x.Adimplente == (pag == null ? x.Adimplente : pag) &&
                                        x.Ativo)
                                .OrderBy(p => p.Nome)
                                .ToList();

            return select;
        }

        public bool NumeroCadastroJaExiste(Pessoa pessoa)
        {
            return _context.Pessoas.Any(p => p.NumeroCadastro == pessoa.NumeroCadastro);
        }

        public async Task<bool> ExistePendencia(int id)
        {
            return await _context.Mensalidades.AnyAsync(m => m.DataVencimento < DateTime.Now && !m.Pago);
        }

        public override async Task Atualizar(Pessoa pessoa)
        {
            var entity = await ObterPorId(pessoa.Id);
            if (entity == null)
                return;

            entity.NumeroCadastro = pessoa.NumeroCadastro;
            entity.Nome = pessoa.Nome;
            entity.RG = pessoa.RG;
            entity.DataNascimento = pessoa.DataNascimento;
            entity.Telefone1 = pessoa.Telefone1;
            entity.Telefone2 = pessoa.Telefone2;
            entity.QuantidadeCasas = pessoa.QuantidadeCasas;
            entity.Logradouro = pessoa.Logradouro;
            entity.Numero = pessoa.Numero;
            entity.Complemento = pessoa.Complemento;
            entity.Bairro = pessoa.Bairro;
            entity.Cep = pessoa.Cep;
            entity.Observacao = pessoa.Observacao;
            entity.Ativo = pessoa.Ativo;
            entity.Isento = pessoa.Isento;
            entity.Imagem = pessoa.Imagem != null ? pessoa.Imagem : entity.Imagem;

            _context.Pessoas.Update(entity);
            await SaveChanges();
        }
    }
}
