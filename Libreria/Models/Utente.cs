using System.ComponentModel.DataAnnotations.Schema;
using Utility;

namespace Libreria.Models;

public class Utente : Entity
{
    public string Nome { get; set; }
}