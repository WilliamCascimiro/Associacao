using Microsoft.AspNetCore.Http;
using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Associacao.App.Models;

namespace Associacao.App.Controllers
{
    [Route("configuracao")]
    public class ConfiguracaoController : Controller
    {
        protected readonly IConfiguracaoRepository _configuracaoRepository;

        public ConfiguracaoController(IConfiguracaoRepository configuracaoRepository)
        {
            _configuracaoRepository = configuracaoRepository;
        }

        [HttpGet]
        [Route("alterar")]
        public IActionResult Index()
        {
            Configuracao configuracao = _configuracaoRepository.Get();
            ConfiguracaoViewModel configuracaoViewModel = new()
            {
                Id = configuracao.Id,
                DataCobrancaInicial = configuracao.DataCobrancaInicial,
                DataCobrancaFinal = configuracao.DataCobrancaFinal,
                ValorMensalidade = configuracao.ValorMensalidade.ToString(),
            };

            return View(configuracaoViewModel);
        }

        
        [HttpPost]
        [Route("alterar")]
        public IActionResult Index(ConfiguracaoViewModel configuracaoViewModel)
        {
            Configuracao configuracao = new()
            {
                Id = configuracaoViewModel.Id,
                DataCobrancaInicial = configuracaoViewModel.DataCobrancaInicial,
                DataCobrancaFinal = configuracaoViewModel.DataCobrancaFinal,
                ValorMensalidade = float.Parse(configuracaoViewModel.ValorMensalidade.Replace(".", "").Replace(",", ".")),
            };

            _configuracaoRepository.Alterar(configuracao);

            return RedirectToAction(nameof(Index));
        }
    }
}
