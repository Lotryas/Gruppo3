using Utility;

namespace Libreria.Models;

public class Utente : Entity
{
    public string Nome { get; set; }
    public string Pass { get; set; }
    public string Ruolo { get; set; }

    public Utente()
    {
        Nome = "";
        Pass = "";
        Ruolo = "";
    }
}