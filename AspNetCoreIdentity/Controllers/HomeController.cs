using AspNetCoreIdentity.Models;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static AspNetCoreIdentity.Extensions.CustomAuthorization;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger; // Logger do kiss log

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Trace("O usuário acessou a home");

            return View();
        }

        public IActionResult Privacy()
        {
            throw new Exception();

            return View();
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }
        
        [Authorize(Policy = "PodeEditar")]
        public IActionResult SecretClaim()
        {
            return View();
        }
        
        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimEscrever()
        {
            return View();
        }
        
        // Bem legal este aqui
        [ClaimsAuthorizeAttribute("Produtos", "Ler")]
        public IActionResult SecretClaimCustom()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Viewer")]
        public IActionResult SecretAll()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}
