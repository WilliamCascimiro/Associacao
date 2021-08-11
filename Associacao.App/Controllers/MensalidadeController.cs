using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Associacao.Interface.Repositories;
using Associacao.App.Models;
using Associacao.Domain.Enums;
using System.Linq;
using Associacao.App.Configuration;
using AutoMapper;

namespace Associacao.App.Controllers
{
    [Route("mensalidade")]
    public class MensalidadeController : Controller
    {
        protected readonly IMensalidadeRepository _mensalidadeRepository;
        protected readonly IPessoaRepository _pessoaRepository;
        protected readonly IMapper _mapper;

        public MensalidadeController(IMensalidadeRepository mensalidadeRepository, IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _mensalidadeRepository = mensalidadeRepository;
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index(DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int statusPagamento)
        {
            string dataVencimentoInicialPadrao = "";
            string dataVencimentoFinalPadrao = "";
            int ultimoDiaDoMes = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            if (dataVencimentoInicial == null)
            {
                dataVencimentoInicialPadrao = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
                dataVencimentoInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
            }
            if (dataVencimentoFinal == null)
            {
                dataVencimentoFinalPadrao = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + ultimoDiaDoMes.ToString();
                dataVencimentoFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, ultimoDiaDoMes);
            }

            ViewBag.listaTipoPagamento = ListaTipoPagamento();
            ViewBag.DataVencimentoInicial = (dataVencimentoInicial is null ? dataVencimentoInicialPadrao : $"{dataVencimentoInicial.Value.Year}-{dataVencimentoInicial.Value.Month.ToString("d2")}-{dataVencimentoInicial.Value.Day.ToString("d2")}");
            ViewBag.DataVencimentoFinal = (dataVencimentoFinal is null ? dataVencimentoFinalPadrao : $"{dataVencimentoFinal.Value.Year}-{dataVencimentoFinal.Value.Month.ToString("d2")}-{dataVencimentoFinal.Value.Day.ToString("d2")}");
            ViewBag.SlcPagamento = statusPagamento;

            return View(_mapper.Map<IEnumerable<MensalidadeViewModel>>(_mensalidadeRepository.Get(dataVencimentoInicial, dataVencimentoFinal, statusPagamento)));
        }

        [Route("mensalidade-por-pessoa/{id?}")]
        public async Task<IActionResult> MensalidadesPorPessoa(int id, DateTime? dataVencimentoInicial, DateTime? dataVencimentoFinal, int? statusPagamento)
        {
            var pessoa = await _pessoaRepository.Detail(id);

            string dataVencimentoInicialPadrao = "";
            string dataVencimentoFinalPadrao = "";

            if (dataVencimentoInicial == null)
            {
                dataVencimentoInicialPadrao = DateTime.Now.Year + "-01" + "-01";
                dataVencimentoInicial = new DateTime(DateTime.Now.Year, 01, 01);
            }
            if (dataVencimentoFinal == null)
            {
                dataVencimentoFinalPadrao = DateTime.Now.Year + "-12" + "-31";
                dataVencimentoFinal = new DateTime(DateTime.Now.Year, 12, 31);
            }

            TempData["listaTipoPagamento"] = ListaTipoPagamento();
            ViewBag.NomePessoa = pessoa.Nome;
            ViewBag.DataVencimentoInicial = (dataVencimentoInicial is null ? dataVencimentoInicialPadrao : $"{dataVencimentoInicial.Value.Year}-{dataVencimentoInicial.Value.Month.ToString("d2")}-{dataVencimentoInicial.Value.Day.ToString("d2")}");
            ViewBag.DataVencimentoFinal = (dataVencimentoFinal is null ? dataVencimentoFinalPadrao : $"{dataVencimentoFinal.Value.Year}-{dataVencimentoFinal.Value.Month.ToString("d2")}-{dataVencimentoFinal.Value.Day.ToString("d2")}");
            ViewBag.SlcPagamento = statusPagamento;

            var mensalidades = _mensalidadeRepository.MensalidadePorPessoa(id, dataVencimentoInicial, dataVencimentoFinal, statusPagamento);

            return View(mensalidades);
        }

        [HttpGet]
        [Route("MensalidadesPorPessoaAnoCorrente")]
        public IActionResult MensalidadesPorPessoaAnoCorrente(int id)
        {
            var mensalidades = _mensalidadeRepository.MensalidadePorPessoaAnoCorrente(id);
            var mensalidadeViewModel = new MensalidadeViewModel().ToListViewModel(mensalidades);

            return new JsonResult(mensalidadeViewModel);
        }

        [HttpPost]
        [Route("pagar-mensalidade")]
        public JsonResult PagarMensalidade(int id)
        {
            return new JsonResult(_mensalidadeRepository.PagarMensalidade(id));
        }

        [HttpPost]
        [Route("reabrir-mensalidade")]
        public IActionResult ReabrirMensalidade(int id)
        {
            return new JsonResult(_mensalidadeRepository.ReabrirMensalidade(id));
        }

        private List<SelectListItem> ListaTipoPagamento()
        {
            return Enum.GetValues(typeof(StatusPagamentoEnum))
                        .Cast<StatusPagamentoEnum>()
                        .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() })
                        .ToList();
        }

    }
}
