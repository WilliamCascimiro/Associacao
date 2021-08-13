using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Associacao.Interface.Services;
using System.Threading.Tasks;

namespace Associacao.Service.Service
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMensalidadeRepository _mensalidadeRepository;
        private readonly IConfiguracaoRepository _configuracaoRepository;

        public PessoaService(IPessoaRepository pessoaRepository, IMensalidadeRepository mensalidadeRepository, IConfiguracaoRepository configuracaoRepository)
        {
            _pessoaRepository = pessoaRepository;
            _mensalidadeRepository = mensalidadeRepository;
            _configuracaoRepository = configuracaoRepository;
        }

        public async Task Cadastrar(Pessoa pessoa)
        {
            if (!await ValidaPessoa(pessoa))
                return;

            await _pessoaRepository.Adcionar(pessoa);
            var config = await _configuracaoRepository.ObterPorId(1);
            _mensalidadeRepository.Create(pessoa, config);
        }

        private async Task<bool> ValidaPessoa(Pessoa pessoa)
        {
            bool passou = true;

            if (_pessoaRepository.NumeroCadastroJaExiste(pessoa))
                return passou = false;

            return passou;
        }


    }
}
