using Libreria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Utility;

namespace Libreria.Controllers
{
    public class LoginController : Controller
    {
        private ILogger<LoginController> ilogger;

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
                ilogger.LogInformation($"UTENTE LOGGATO: {email}");

                return Redirect("/Login/Profilo");
            }

            return Redirect("Index");
        }

        public IActionResult Profilo()
        {
            if (HttpContext.Items["AuthUser"] is null)
            {
                return Redirect("/Login/Index");
            }
            Utente? u = (Utente?)HttpContext.Items["AuthUser"];
            List<Entity> prestati = new();
            if (u is not null)
                prestati = DAOUtente_Libro.GetInstance().FindPrestati(u.Id);
            return View(prestati);
        }

        public IActionResult Logout()
        {
            Utente? user = (Utente?)HttpContext.Items["AuthUser"];

            Response.Cookies.Delete("auth");
            ilogger.LogInformation($"LOGOUT: {user?.Email}");

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
                ilogger.LogInformation($"UTENTE LOGGATO: {utente.Email}");

                return Redirect("/Login/Profilo");
            }
            else
            {
                return Content("Registrazione fallita");
            }
        }

        [HttpGet("/Login/Restituisci/{idUtente:int}/{idLibro:int}")]
        public IActionResult Restituisci(long idUtente, long idLibro)
        {
            if (DAOUtente_Libro.GetInstance().DeletePrestato(idUtente, idLibro))
            {
                return Redirect("/Login/Profilo");
            }
            else
            {
                return Content("Errore nella cancellazione del prestito");
            }
        }
    }
}
