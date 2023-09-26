using System.Data.SqlClient;
using Utility;

namespace Libreria.Models;

public class DAOUtente_Libro : IDAO
{
    private readonly Database _db;
    private static DAOUtente_Libro? _instance;

    private DAOUtente_Libro()
    {
        _db = new(Config.ConnectionString.Value);
    }

    public static DAOUtente_Libro GetInstance()
    {
        return _instance ??= new DAOUtente_Libro();
    }

    public bool Delete(long id)
    {
        SqlCommand cmd = new("DELETE FROM Utenti_Libri WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);
        return _db.ExecQuery(cmd);
    }

    public Entity? Find(long id)
    {
        SqlCommand cmd = new("SELECT * FROM Utenti_Libri WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);

        Dictionary<string, object>? riga = _db?.ReadOne(cmd);
        if (riga is null) return null;

        Utente_Libro ul = new();
        ul.Id = (long)riga["id"];
        ul.Utente = (Utente?)DAOUtente.GetInstance().Find((long)riga["idutente"]);
        ul.Libro = (Libro?)DAOLibro.GetInstance().Find((long)riga["idlibro"]);

        return ul;
    }

    public bool Insert(Entity entity)
    {
        Utente_Libro ul = (Utente_Libro)entity;

        SqlCommand cmd = new(@"
            INSERT INTO Utenti_Libri (idUtente, idLibro)
            VALUES (@idUtente, @idLibro);
        ");
        cmd.Parameters.AddWithValue("@idUtente", ul.Utente?.Id);
        cmd.Parameters.AddWithValue("@idLibro", ul.Libro?.Id);

        return _db.ExecQuery(cmd);
    }

    public List<Entity> ReadAll()
    {
        SqlCommand cmd = new("SELECT * FROM Utenti_Libri;");
        List<Dictionary<string, object>> tabella = _db.ReadMany(cmd);

        List<Entity> ris = new();
        foreach (Dictionary<string, object> riga in tabella)
        {
            Utente_Libro ul = new();
            ul.Id = (long)riga["id"];
            ul.Utente = (Utente?)DAOUtente.GetInstance().Find((long)riga["idutente"]);
            ul.Libro = (Libro?)DAOLibro.GetInstance().Find((long)riga["idlibro"]);
            ris.Add(ul);
        }

        return ris;
    }

    public bool Update(Entity entity)
    {
        Utente_Libro ul = (Utente_Libro)entity;

        SqlCommand cmd = new(@"
            UPDATE Utenti_Libri SET
                idUtente = @idUtente,
                idLibro = @idLibro
            WHERE id = @id;
        ");
        cmd.Parameters.AddWithValue("@id", ul.Id);
        cmd.Parameters.AddWithValue("@idUtente", ul.Utente?.Id);
        cmd.Parameters.AddWithValue("@idLibro", ul.Libro?.Id);

        return _db.ExecQuery(cmd);
    }

    public List<Entity> FindPrestati(long idUtente)
    {
        Console.WriteLine(idUtente);
        SqlCommand cmd = new("SELECT * FROM Utenti_Libri WHERE idUtente = @id;");
        cmd.Parameters.AddWithValue("@id", idUtente);
        List<Dictionary<string, object>> tabella = _db.ReadMany(cmd);
        List<Entity> ris = new();
        foreach (Dictionary<string, object> riga in tabella)
        {
            Utente_Libro ul = new();
            Console.WriteLine(riga["idlibro"]);
            ul.Libro = (Libro?)DAOLibro.GetInstance().Find((int)riga["idlibro"]);
            ris.Add(ul);
        }
        return ris;

    }
}
