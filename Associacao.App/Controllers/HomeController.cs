using Associacao.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Associacao.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3, 3)}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int id)
        {
            var modelErrro = new ErrorViewModel();

            if (id == 500)
            {
                modelErrro.Titulo = "Ocorreu um erro";
                modelErrro.Message = "Ocorreu um erro";
                modelErrro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErrro.Titulo = "Ops! Página não encontrada.";
                modelErrro.Message = "Ops! Página não encontrada.";
                modelErrro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErrro.Message = "Acesso negado";
                modelErrro.Titulo = "Acesso negado";
                modelErrro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View(modelErrro);
        }
    }
}
