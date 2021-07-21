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
    public class PessoaController : Controller
    {
        protected readonly IServiceProvider _serviceProvider;

        public PessoaController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // GET: PessoaController
        public ActionResult Index()
        {
            var rep = (IPessoaRepository)_serviceProvider.GetService(typeof(IPessoaRepository));
            var pessoa = rep.GetComplete();
            return View(pessoa);
        }

        // GET: PessoaController/Details/5
        public ActionResult Details(int id)
        {
            var pessoaRepository = (IPessoaRepository)_serviceProvider.GetService(typeof(IPessoaRepository));
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            var pessoa = pessoaRepository.Detail(id);
            var mensalidades = mensalidadeRepository.PorPessoa(id).Take(10);

            ViewBag.Ativo = pessoa.Ativo == true ? "Ativo" : "Desativado";

            var p = new PessoaMensalidadeViewModel() { Pessoa = pessoa, Mensalidades = mensalidades };

            //return View(pessoa);
            return View(p);
        }

        [HttpGet]
        public JsonResult ExistePendencia(int id)
        {
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            return new JsonResult(mensalidadeRepository.ExistePendencia(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            var pessoaRepo = (IPessoaRepository)_serviceProvider.GetService(typeof(IPessoaRepository));
            var mensalidadeRepo = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            
            pessoaRepo.Create(pessoa);
            mensalidadeRepo.Create(pessoa.Id, pessoa.QuantidadeCasas);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController/Edit/5
        public ActionResult Edit(int id)
        {
            var pessoaRepository = (IPessoaRepository)_serviceProvider.GetService(typeof(IPessoaRepository));
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            var pessoa = pessoaRepository.Detail(id);
            var mensalidades = mensalidadeRepository.PorPessoa(id).Take(10);

            ViewBag.Ativo = pessoa.Ativo == true ? "Ativo" : "Desativado";

            var p = new PessoaMensalidadeViewModel() { Pessoa = pessoa, Mensalidades = mensalidades };

            //return View(pessoa);
            return View(p);
        }

        // POST: PessoaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pessoa pessoa)
        {
            var pessoaRepository = (IPessoaRepository)_serviceProvider.GetService(typeof(IPessoaRepository));
            pessoaRepository.Alterar(pessoa);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
