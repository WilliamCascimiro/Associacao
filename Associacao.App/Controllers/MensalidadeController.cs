using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Associacao.Domain.Entities;
using Associacao.Repository.Common;
using Associacao.Interface.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Associacao.App.Controllers
{
    public class MensalidadeController : Controller
    {
        protected readonly IServiceProvider _serviceProvider;

        public MensalidadeController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // GET: Mensalidade
        //public async Task<IActionResult> Index()
        //{
        //    //var rep = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
        //    //return View(await applicationDbContext.ToListAsync());

        //    //var applicationDbContext = _context.Mensalidades.Include(m => m.Pessoa).Select(m => new { m.DataVencimento, m.Pessoa.Nome });
        //    //return View(await applicationDbContext.ToListAsync());
        //}

        public IActionResult MensalidadesPorPessoa(int id)
        {
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            var pessoaRepository = (IPessoaRepository)_serviceProvider.GetService(typeof(IPessoaRepository));

            var pessoa = pessoaRepository.Detail(id);
            var mensalidades = mensalidadeRepository.MensalidadePorPessoaAnoCorrente(id);

            ViewBag.NomePessoa = pessoa.Nome;

            return View(mensalidades);
        }

        [HttpGet]
        public JsonResult MensalidadesPorPessoaGet(int id)
        {
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            var mensalidades = mensalidadeRepository.MensalidadePorPessoaAnoCorrente(id);

            return new JsonResult(mensalidades);
        }

        //[HttpPost]
        //public JsonResult MensalidadesPorPessoaPost()
        //{
        //    var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
        //    var mensalidades = mensalidadeRepository.MensalidadePorPessoaAnoCorrente(10);

        //    return new JsonResult(mensalidades);
        //}

        // GET: Mensalidade/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));

            if (id == null)
            {
                return NotFound();
            }

            var mensalidade = mensalidadeRepository.Detail(id);
            if (mensalidade == null)
            {
                return NotFound();
            }

            return View(mensalidade);
        }

        [HttpPost]
        public JsonResult PagarMensalidade(int id)
        {
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            return new JsonResult(mensalidadeRepository.PagarMensalidade(id));
        }

        [HttpPost]
        public JsonResult ReabrirMensalidade(int id)
        {
            var mensalidadeRepository = (IMensalidadeRepository)_serviceProvider.GetService(typeof(IMensalidadeRepository));
            return new JsonResult(mensalidadeRepository.ReabrirMensalidade(id));
        }

        //// GET: Mensalidade/Create
        //public IActionResult Create()
        //{
        //    ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "Id", "Bairro");
        //    return View();
        //}

        // POST: Mensalidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,DataVencimento,DataPagamento,Pago,IdPessoa")] Mensalidade mensalidade)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(mensalidade);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "Id", "Bairro", mensalidade.IdPessoa);
        //    return View(mensalidade);
        //}

        // GET: Mensalidade/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var mensalidade = await _context.Mensalidades.FindAsync(id);
        //    if (mensalidade == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "Id", "Bairro", mensalidade.IdPessoa);
        //    return View(mensalidade);
        //}

        //// POST: Mensalidade/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,DataVencimento,DataPagamento,Pago,IdPessoa")] Mensalidade mensalidade)
        //{
        //    if (id != mensalidade.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(mensalidade);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MensalidadeExists(mensalidade.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "Id", "Bairro", mensalidade.IdPessoa);
        //    return View(mensalidade);
        //}

        //// GET: Mensalidade/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var mensalidade = await _context.Mensalidades
        //        .Include(m => m.Pessoa)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (mensalidade == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(mensalidade);
        //}

        //// POST: Mensalidade/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var mensalidade = await _context.Mensalidades.FindAsync(id);
        //    _context.Mensalidades.Remove(mensalidade);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MensalidadeExists(int id)
        //{
        //    return _context.Mensalidades.Any(e => e.Id == id);
        //}
    }
}
