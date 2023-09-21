using Libreria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Libreria.Controllers
{
    public class LoginController : Controller
    {
        private ILogger<LoginController> ilogger;
        private static Utente? utenteLoggato = null;
        private static int chiamata = -1;

        public LoginController(ILogger<LoginController> logger) 
        { 
            ilogger = logger;
        }

        public IActionResult Index()
        {
            chiamata++;
            ilogger.LogInformation($"TENTATIVO NUMERO:{chiamata}");
            return View(chiamata);
        }

        public IActionResult Valida(Dictionary<string, string> keyValuePairs)
        {
           
            if (DAOUtente.GetInstance().Validate(keyValuePairs["nome"], keyValuePairs["pass"]))
            {

                ilogger.LogInformation($"UTENTE LOGGATO: {keyValuePairs["user"]}");             
                utenteLoggato = (Utente?)DAOUtente.GetInstance().Find(keyValuePairs["user"]);    
                return View("Views/Login/Profilo.cshtml", utenteLoggato);                         
            }
            else
            {
                return Redirect("Index"); 
            }

        }

        public IActionResult Logout()
        {
            
            chiamata = -1;

            ilogger.LogInformation($"LOGOUT: {utenteLoggato.Nome}");

            utenteLoggato = null;

            return Redirect("Index");

        }

        public IActionResult Registrazione()
        {
            return View();
        }

        public IActionResult Salva(Dictionary<string, object> keyValuePairs)
        {
            Utente utente = new Utente();
            utente.PopulateFromRecord(keyValuePairs);
            chiamata--;

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
