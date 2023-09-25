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
            if (HttpContext.Items["AuthUser"] is not null)
            {
                return Redirect("/");
            }

            return View();
        }

        public IActionResult Valida()
        {
            var email = Request.Form["email"];
            var password = Request.Form["pass"];

            if (DAOUtente.GetInstance().Validate(email, password))
            {
                CookieOptions opts = new();
                opts.Expires = DateTime.Now.AddYears(1);
                opts.HttpOnly = true;
                opts.Secure = true;
                opts.SameSite = SameSiteMode.Lax;

                Response.Cookies.Append("auth", email, opts);

                utenteLoggato = (Utente?)DAOUtente.GetInstance().Find(email);
                ilogger.LogInformation($"UTENTE LOGGATO: {email}");

                return View("Views/Login/Profilo.cshtml", utenteLoggato);
            }

            return Redirect("Index");
        }

        public IActionResult Profilo()
        {
            if (HttpContext.Items["AuthUser"] is not null)
            {
                utenteLoggato = (Utente?)HttpContext.Items["AuthUser"];
            }
            return View(utenteLoggato);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth");
            ilogger.LogInformation($"LOGOUT: {utenteLoggato?.Email}");
            utenteLoggato = null;
            return Redirect("Index");

        }

        public IActionResult Registrazione()
        {
            if (HttpContext.Items["AuthUser"] is not null)
            {
                return Redirect("/");
            }

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
                CookieOptions opts = new();
                opts.Expires = DateTime.Now.AddYears(1);
                opts.HttpOnly = true;
                opts.Secure = true;
                opts.SameSite = SameSiteMode.Lax;

                Response.Cookies.Append("auth", utente.Email, opts);

                utenteLoggato = DAOUtente.GetInstance().Find(utente.Email) as Utente;
                ilogger.LogInformation($"UTENTE LOGGATO: {utente.Email}");

                return View("Views/Login/Profilo.cshtml", utenteLoggato);
            }
            else
            {
                return Content("Registrazione fallita");
            }
        }
    }
}
