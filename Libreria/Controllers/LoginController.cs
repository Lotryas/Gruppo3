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

        public IActionResult Valida(Dictionary<string, string> keyValuePairs)
        {
           
            if (DAOUtente.GetInstance().Validate(keyValuePairs["nome"], keyValuePairs["pass"]))
            {

                ilogger.LogInformation($"UTENTE LOGGATO: {keyValuePairs["nome"]}");             
                utenteLoggato = (Utente?)DAOUtente.GetInstance().Find(keyValuePairs["nome"]);    
                return View("Views/Login/Profilo.cshtml", utenteLoggato);                         
            }
            else
            {
                return Redirect("Index"); 
            }

        }

        public IActionResult Logout()
        {
            
         

            ilogger.LogInformation($"LOGOUT: {utenteLoggato.Nome}");

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
