using Utility;

namespace Libreria.Models;

public class Utente : Entity
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Pass { get; set; }
    public string Ruolo { get; set; }

    public Utente()
    {
        Nome = "";
        Email = "";
        Pass = "";
        Ruolo = "";
    }

    public Utente(int id,string nome, string email, string pass, string ruolo): base (id)
    {
        Nome = nome;
        Email = email;
        Pass = pass;
        Ruolo = ruolo;
    }
}