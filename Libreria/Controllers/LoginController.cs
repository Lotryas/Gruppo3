using Libreria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Libreria.Controllers
{
    public class LoginController : Controller
    {
        private ILogger<LoginController> ilogger;
        private static Utente? utenteLoggato = null;


        public LoginController(ILogger<LoginController> logger)
        {
            ilogger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Valida()
        {
            var email = Request.Form["email"];
            var password = Request.Form["pass"];

            if (DAOUtente.GetInstance().Validate(email, password))
            {
                ilogger.LogInformation($"UTENTE LOGGATO: {email}");
                utenteLoggato = (Utente?)DAOUtente.GetInstance().Find(email);
                return View("Views/Login/Profilo.cshtml", utenteLoggato);
            }
            else
            {
                return Redirect("Index");
            }

        }

        public IActionResult Logout()
        {
            ilogger.LogInformation($"LOGOUT: {utenteLoggato?.Email}");
            utenteLoggato = null;
            return Redirect("Index");

        }

        public IActionResult Registrazione()
        {
            return View();
        }

        public IActionResult Salva()
        {
            Utente utente = new Utente();
            utente.Nome = Request.Form["nome"];
            utente.Email = Request.Form["email"];
            utente.Pass = Request.Form["pass"];

            if (DAOUtente.GetInstance().Insert(utente))
            {
                return Content("Registrazione avvenuta con successo");
            }
            else
            {
                return Content("Registrazione fallita");
            }
        }
    }
}
