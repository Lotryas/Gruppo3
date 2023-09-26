using Utility;

namespace Libreria.Models
{
    public class Utente_Libro : Entity
    {

        public Utente? Utente { get; set; }
        public Libro? Libro { get; set; }
        public Utente_Libro(long id, Utente? utente, Libro? libro): base(id)
        {
            Utente = utente;
            Libro = libro;
        }
        public Utente_Libro() { }

    }
}
