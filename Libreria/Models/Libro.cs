using Utility;

namespace Libreria.Models
{
    public class Libro : Entity
    {
        public string Titolo { get; set; }
        public string Autore { get;set; }
        public string Genere { get; set; }
        public int Quantita { get; set; }
        public bool Formato { get; set; }
        public string NomeFile { get; set; }
        public string Locandina { get; set; }

      public Libro() 
        {
            Titolo = "";
            Autore = "";
            Genere = "";
            NomeFile = "";
            Locandina = "";
        }

        public Libro(long id, string titolo, string autore, string genere, int quantita, bool formato, string nomeFile,string locandina) : base(id)
        {
            Titolo = titolo;
            Autore = autore;
            Genere = genere;
            Quantita = quantita;
            Formato = formato;
            NomeFile = nomeFile;
            Locandina = locandina;
        }
    }
}
