using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Associacao.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associacao.Service.Service
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMensalidadeRepository _mensalidadeRepository;

        public PessoaService(IPessoaRepository pessoaRepository, IMensalidadeRepository mensalidadeRepository)
        {
            _pessoaRepository = pessoaRepository;
            _mensalidadeRepository = mensalidadeRepository;
        }

        public async Task Cadastrar(Pessoa pessoa)
        {
            if (!await ValidaPessoa(pessoa))
                return;

            await _pessoaRepository.Adcionar(pessoa);
            _mensalidadeRepository.Create(pessoa.Id, pessoa.QuantidadeCasas);
        }

        private async Task<bool> ValidaPessoa(Pessoa pessoa)
        {
            bool passou = true;

            if (!_pessoaRepository.NumeroCadastroDisponivel(pessoa))
                return passou = false;

            return passou;
        }


    }
}
