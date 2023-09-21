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

      public Libro() 
        {
            Titolo = "";
            Autore = "";
            Genere = "";
            NomeFile = "";
        }

        public Libro(int id, string titolo, string autore, string genere, int quantita, bool formato, string nomeFile) : base(id)
        {
            Titolo = titolo;
            Autore = autore;
            Genere = genere;
            Quantita = quantita;
            Formato = formato;
            NomeFile = nomeFile;
        }
    }
}
