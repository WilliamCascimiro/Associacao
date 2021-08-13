using Associacao.Domain.Entities;
using Associacao.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Associacao.App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using Associacao.Interface.Services;

namespace Associacao.App.Controllers
{
    [Route("pessoa")]
    public class PessoaController : BaseController
    {
        protected readonly IPessoaRepository _pessoaRepository;
        protected readonly IMensalidadeRepository _mensalidadeRepository;
        protected readonly IPessoaService _pessoaService;
        protected readonly IMapper _mapper;

        public PessoaController(IPessoaRepository pessoaRepository, IMensalidadeRepository mensalidadeRepository, IMapper mapper, IPessoaService pessoaService)
        {
            _pessoaRepository = pessoaRepository;
            _mensalidadeRepository = mensalidadeRepository;
            _pessoaService = pessoaService;
            _mapper = mapper;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(string cadastro, string nome, int statusPagamento)
        {
            ViewBag.listaStatusPagamento = ListaStatusPagamento();
            ViewBag.Cadastro = cadastro;
            ViewBag.Nome = nome;
            ViewBag.StatusPagamento = statusPagamento;

            return View(_mapper.Map<IEnumerable<PessoaViewModel>>(await _pessoaRepository.GetComplete(cadastro, nome, statusPagamento)));
        }

        [HttpGet]
        [Route("detalhe/{id?}")]
        public async Task<IActionResult> Details(int id)
        {
            return View(_mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPorId(id)));
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
        public async Task<IActionResult> Create(PessoaViewModel pessoaViewModel)
        {
            if (!ModelState.IsValid) return View(pessoaViewModel);
            var pessoa = _mapper.Map<Pessoa>(pessoaViewModel);

            if (pessoaViewModel.ImagemUpload != null && !String.IsNullOrEmpty(pessoaViewModel.ImagemUpload.FileName))
            {
                var fileName = Guid.NewGuid() + "_" + pessoaViewModel.ImagemUpload.FileName;
                if(!await UploadArquivo(pessoaViewModel.ImagemUpload, fileName))
                    return View(pessoaViewModel);
                pessoa.Imagem = fileName;
            }

            await _pessoaService.Cadastrar(pessoa);            
            return RedirectToAction(nameof(Index));
        }

        [Route("editar/{id?}")]
        public async Task<IActionResult> Edit(int id)
        {
            var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaRepository.Detail(id));

            if (pessoaViewModel == null) return NotFound();

            return View(pessoaViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("editar/{id?}")]
        public async Task<IActionResult> Edit(int id, PessoaViewModel pessoaViewModel)
        {
            if (id != pessoaViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(pessoaViewModel);

            if (pessoaViewModel.ImagemUpload != null && !String.IsNullOrEmpty(pessoaViewModel.ImagemUpload.FileName))
            {
                var fileName = Guid.NewGuid() + "_" + pessoaViewModel.ImagemUpload.FileName;
                if (!await UploadArquivo(pessoaViewModel.ImagemUpload, fileName))
                    return View(pessoaViewModel);

                pessoaViewModel.Imagem = fileName;
            }

            await _pessoaRepository.Atualizar(_mapper.Map<Pessoa>(pessoaViewModel));
            
            return RedirectToAction(nameof(Index));
            

            //var pessoa = _mapper.Map<Pessoa>(pessoaViewModel);
            //await _pessoaRepository.Atualizar(pessoa);

            //return RedirectToAction(nameof(Index));
        }

        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPorId(id));

            if (pessoaViewModel == null) return NotFound();

            return View(pessoaViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPorId(id));
            
            if (pessoaViewModel == null) return NotFound();

            await _pessoaRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("ExistePendencia")]
        public IActionResult ExistePendencia(int id)
        {
            return new JsonResult(_pessoaRepository.ExistePendencia(id));
        }

        

        private List<SelectListItem> ListaStatusPagamento()
        {
            return  new List<SelectListItem> {
                        new SelectListItem() { Value = "0", Text = "Todos" },
                        new SelectListItem() { Value = "1", Text = "Em dia" },
                        new SelectListItem() { Value = "2", Text = "Em atraso" }
                    };
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string fileName)
        {
            if (arquivo.Length <= 0)
                return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", fileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream (path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

    }
}
