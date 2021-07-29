using Microsoft.AspNetCore.Http;
using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Associacao.App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Associacao.App.Controllers
{
    [Route("pessoa")]
    public class PessoaController : Controller
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IPessoaRepository _pessoaRepository;
        protected readonly IMensalidadeRepository _mensalidadeRepository;

        public PessoaController(IServiceProvider serviceProvider, IPessoaRepository pessoaRepository, IMensalidadeRepository mensalidadeRepository)
        {
            _serviceProvider = serviceProvider;
            _pessoaRepository = pessoaRepository;
            _mensalidadeRepository = mensalidadeRepository;
        }

        [Route("")]
        [Route("Index")]
        //public IActionResult Index(string id, string categoria)
        public IActionResult Index(string cadastro, string nome, int slcPagamento)
        {
            var statusPagamento = new List<SelectListItem> {
                new SelectListItem() { Value = "0", Text = "Todos" },
                new SelectListItem() { Value = "1", Text = "Em dia" },
                new SelectListItem() { Value = "2", Text = "Em atraso" }
            };

            ViewBag.Cadastro = cadastro;
            ViewBag.Nome = nome;
            ViewBag.SlcPagamento = slcPagamento;

            //ViewBag.poste_id = new SelectList(db.Postes, "Id", "designation");
            //view


            TempData["statusPagamento"] = statusPagamento;

            var pessoa = _pessoaRepository.GetComplete(cadastro, nome, slcPagamento);
            return View(pessoa);
        }

        [Route("detalhe/{id?}")]
        // GET: PessoaController/Details/5
        public ActionResult Details(int id)
        {
            var pessoa = _pessoaRepository.Detail(id);
            var mensalidades = _mensalidadeRepository.PorPessoa(id).Take(10);

            ViewBag.Ativo = pessoa.Ativo == true ? "Ativo" : "Desativado";

            var p = new PessoaMensalidadeViewModel() { Pessoa = pessoa, Mensalidades = mensalidades };

            //return View(pessoa);
            return View(p);
        }

        [HttpGet]
        [Route("ExistePendencia")]
        public IActionResult ExistePendencia(int id)
        {
            return new JsonResult(_mensalidadeRepository.ExistePendencia(id));
        }

        [HttpGet]
        [Route("cadastrar")]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Create(PessoaViewModel pessoa)
        {
            if (!ModelState.IsValid)
            {
                return View(pessoa);
            }

            //foreach (var error in ModelState.Values.SelectMany(x => x.Errors))
            //{
            //    Console.WriteLine(error.ErrorMessage);
            //}

            //_pessoaRepository.Create(pessoa);
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
        public ActionResult Edit(int id)
        {
            //var pessoaRepository = (IPessoaRepository)_serviceProvider.GetService(typeof(IPessoaRepository));
            //var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            //var pessoa = pessoaRepository.Detail(id);
            //var mensalidades = mensalidadeRepository.PorPessoa(id).Take(10);

            //ViewBag.Ativo = pessoa.Ativo == true ? "Ativo" : "Desativado";

            //var p = new PessoaMensalidadeViewModel() { Pessoa = pessoa, Mensalidades = mensalidades };

            var pessoa = _pessoaRepository.Detail(id);

            ViewBag.Ativo = pessoa.Ativo == true ? "Ativo" : "Desativado";

            var p = new PessoaViewModel() 
            {
                Id = pessoa.Id,
                NumeroCadastro = pessoa.NumeroCadastro,
                Nome = pessoa.Nome,
                RG = pessoa.RG,
                DataNascimento = pessoa.DataNascimento,
                Telefone1 = pessoa.Telefone1,
                Telefone2 = pessoa.Telefone2,
                Bairro = pessoa.Bairro,
                Logradouro = pessoa.Logradouro,
                Numero = pessoa.Numero,
                Complemento = pessoa.Complemento,
                CEP = pessoa.Cep,
                Observacao = pessoa.Observacao,
                QuantidadeCasas = pessoa.QuantidadeCasas,
                Ativo = pessoa.Ativo,
                Isento = pessoa.Isento
            };

            //return View(pessoa);
            return View(p);
        }

        //public IActionResult Download()
        //{
        //    var fileBytes = System.IO.File.ReadAllBytes(@"");
        //    var fileName = "arquivo.txt";
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public ActionResult Edit(int id, Pessoa pessoa)
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
        // GET: PessoaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
