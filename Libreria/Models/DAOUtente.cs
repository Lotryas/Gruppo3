using System.Data.SqlClient;
using Utility;

namespace Libreria.Models;

public class DAOUtente : IDAO
{
    private readonly Database _db;
    private static DAOUtente? _instance;

    private DAOUtente()
    {
        _db = new Database(Config.ConnectionString.Value);
    }

    public static DAOUtente GetInstance()
    {
        return _instance ??= new DAOUtente();
    }

    public bool Delete(long id)
    {
        SqlCommand cmd = new("DELETE FROM Utenti WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);
        return _db.ExecQuery(cmd);
    }

    public Entity? Find(long id)
    {
        SqlCommand cmd = new("SELECT TOP 1 * FROM Utenti WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);

        Dictionary<string, object>? record = _db.ReadOne(cmd);
        if (record is null) return null;

        Utente utente = new();
        utente.PopulateFromRecord(record);

        return utente;
    }

    public Entity? Find(string email)
    {
        SqlCommand cmd = new("SELECT TOP 1 * FROM Utenti WHERE email = @email;");
        cmd.Parameters.AddWithValue("@email", email);

        Dictionary<string, object>? record = _db.ReadOne(cmd);
        if (record is null) return null;

        Utente utente = new();
        utente.PopulateFromRecord(record);

        return utente;
    }

    public bool Insert(Entity entity)
    {
        Utente utente = (Utente)entity;

        SqlCommand cmd = new(@"
            INSERT INTO Utenti (nome, email, pass, ruolo)
            VALUES (@nome, @email, HASHBYTES('SHA2_512', @pass), @ruolo); 
        ");
        cmd.Parameters.AddWithValue("@nome", utente.Nome);
        cmd.Parameters.AddWithValue("@email", utente.Email);
        cmd.Parameters.AddWithValue("@pass", utente.Pass);
        cmd.Parameters.AddWithValue("@ruolo", "Dipendente");

        return _db.ExecQuery(cmd);
    }

    public List<Entity> ReadAll()
    {
        SqlCommand cmd = new("SELECT * FROM Utenti ORDER BY id DESC;");
        List<Dictionary<string, object>> records = _db.ReadMany(cmd);

        List<Entity> utenti = new();
        foreach (Dictionary<string, object> record in records)
        {
            Utente utente = new();
            utente.PopulateFromRecord(record);
            utenti.Add(utente);
        }

        return utenti;
    }

    public bool Update(Entity entity)
    {
        Utente utente = (Utente)entity;

        SqlCommand cmd = new(@"
            UPDATE Utenti SET
                nome = @nome,
                email = @email,
                pass = HASHBYTES('SHA2_512', @pass),
                ruolo = @ruolo
            WHERE id = @id;
        ");
        cmd.Parameters.AddWithValue("@nome", utente.Nome);
        cmd.Parameters.AddWithValue("@email", utente.Email);
        cmd.Parameters.AddWithValue("@pass", utente.Pass);
        cmd.Parameters.AddWithValue("@ruolo", utente.Ruolo);
        cmd.Parameters.AddWithValue("@id", utente.Id);

        return _db.ExecQuery(cmd);
    }

    public bool Validate(string email, string pass)
    {
        SqlCommand cmd = new(@"
            SELECT TOP 1 * FROM Utenti
            WHERE email = @email
            AND pass = HASHBYTES('SHA2_512', @pass);
        ");
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@pass", pass);

        Dictionary<string, object>? record = _db.ReadOne(cmd);
        if (record is null) return false;
        return true;
    }
}