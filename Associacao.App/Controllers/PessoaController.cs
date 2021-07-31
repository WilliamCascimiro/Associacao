using Microsoft.AspNetCore.Http;
using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Associacao.App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Associacao.App.Controllers
{
    [Route("pessoa")]
    public class PessoaController : Controller
    {
        protected readonly IPessoaRepository _pessoaRepository;
        protected readonly IMensalidadeRepository _mensalidadeRepository;

        public PessoaController(IPessoaRepository pessoaRepository, IMensalidadeRepository mensalidadeRepository)
        {
            _pessoaRepository = pessoaRepository;
            _mensalidadeRepository = mensalidadeRepository;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index(string cadastro, string nome, int statusPagamento)
        {
            //Preenche o drop down da tela
            TempData["listaStatusPagamento"] = ListaStatusPagamento();

            //Persiste os filtros de consulta
            ViewBag.Cadastro = cadastro;
            ViewBag.Nome = nome;
            ViewBag.StatusPagamento = statusPagamento;

            var pessoa = _pessoaRepository.GetComplete(cadastro, nome, statusPagamento);
            return View(pessoa);
        }

        [Route("detalhe/{id?}")]
        public ActionResult Details(int id)
        {
            var pessoa = _pessoaRepository.Detail(id);
            var mensalidades = _mensalidadeRepository.PorPessoa(id).Take(10);

            ViewBag.Ativo = pessoa.Ativo == true ? "Ativo" : "Desativado";

            var pessoaMensalidadeViewModel = new PessoaMensalidadeViewModel() { Pessoa = pessoa, Mensalidades = mensalidades };

            return View(pessoaMensalidadeViewModel);
        }

        [HttpGet]
        [Route("ExistePendencia")]
        public IActionResult ExistePendencia(int id)
        {
            return new JsonResult(_mensalidadeRepository.ExistePendencia(id));
        }

        [HttpGet]
        [Route("cadastrar")]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Create(PessoaViewModel pessoaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pessoaViewModel);
            }

            Pessoa pessoa = pessoaViewModel.ToEntity();

            _pessoaRepository.Create(pessoa);
            _mensalidadeRepository.Create(pessoa.Id, pessoa.QuantidadeCasas);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("editar/{id?}")]
        public IActionResult Edit(int id)
        {
            return View(new PessoaViewModel().ToViewModel(_pessoaRepository.Detail(id)));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(int id, Pessoa pessoa)
        {
            _pessoaRepository.Alterar(pessoa);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaController/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<SelectListItem> ListaStatusPagamento()
        {
            return 
                new List<SelectListItem> {
                        new SelectListItem() { Value = "0", Text = "Todos" },
                        new SelectListItem() { Value = "1", Text = "Em dia" },
                        new SelectListItem() { Value = "2", Text = "Em atraso" }
                    };
        }

    }
}
