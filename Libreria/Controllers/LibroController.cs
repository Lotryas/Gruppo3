using Libreria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.Controllers
{
    public class LibroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AggiungiLibri()
        {
            return View();
        }

        public IActionResult Aggiungi(Dictionary<string,string> parametri)
        {
            Console.WriteLine(parametri["titolo"]);
            Libro l = new();
            l.Titolo = parametri["titolo"];
            l.Autore = parametri["autore"];
            l.Genere = parametri["genere"];
            l.Quantita = int.Parse(parametri["quantita"]);
            l.Formato = bool.Parse(parametri["formato"]);
            l.NomeFile = parametri["nomefile"];
            if (DAOLibro.GetInstance().Insert(l))
                return Content("Libro aggiunto al database!");
            else
                return Content("Errore nell'inserimento");
        }
    }
}
