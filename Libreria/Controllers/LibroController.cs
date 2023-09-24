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

        public IActionResult Aggiungi(Dictionary<string, string> parametri)
        {
            Libro l = new();
            l.Titolo = parametri["titolo"];
            l.Autore = parametri["autore"];
            l.Genere = parametri["genere"];
            l.Quantita = int.Parse(parametri["quantita"]);
            l.Formato = bool.Parse(parametri["formato"]);
            l.NomeFile = parametri["nomefile"];
            l.Locandina = parametri["locandina"];
            if (DAOLibro.GetInstance().Insert(l))
                return Content("Libro aggiunto al database!");
            else
                return Content("Errore nell'inserimento");
        }

        public IActionResult ModificaLibri(long id)
        {
            var libro = (Libro?)DAOLibro.GetInstance().Find(id);
            if (libro is null)
                return Content($"Libro con ID {id} non trovato");

            return View(libro);
        }

        public IActionResult Modifica(Dictionary<string, string> parametri)
        {
            Libro l = new();
            l.Id = long.Parse(parametri["id"]);
            l.Titolo = parametri["titolo"];
            l.Autore = parametri["autore"];
            l.Genere = parametri["genere"];
            l.Quantita = int.Parse(parametri["quantita"]);
            l.Formato = bool.Parse(parametri["formato"]);
            l.NomeFile = parametri["nomefile"];
            l.Locandina = parametri["locandina"];
            Console.WriteLine(l.ToString());
            if (DAOLibro.GetInstance().Update(l))
                return Content("Libro aggiornato con successo!");
            else
                return Content("Errore nell'aggiornamento");
        }

        public IActionResult Delete(long id)
        {
            if (DAOLibro.GetInstance().Delete(id))
                // Sarà eventualmente da cambiare per tornare all'elenco
                return Redirect("/Home/Elenco");
            else
                return Content("Errore nella cancellazione");
        }
    }
}
