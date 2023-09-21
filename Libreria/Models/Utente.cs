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

    public Utente(int id,string nome, string pass, string ruolo): base (id)
    {
        Nome = nome;
        Pass = pass;
        Ruolo = ruolo;
    }
}