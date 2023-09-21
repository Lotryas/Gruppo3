namespace Libreria.Models
{
    public class Libro
    {
        public string? Titolo { get; set; }
        public string? Autore { get;set; }
        public string? Genere { get; set; }
        public int Quantita { get; set; }
        public bool Formato { get; set; }
        public string? NomeFile { get; set; }
    }
}
