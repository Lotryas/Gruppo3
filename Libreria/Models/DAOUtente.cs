using System.Data.SqlClient;
using Utility;

namespace Libreria.Models;

public class DAOUtente
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

    public Entity? Find(string nome)
    {
        SqlCommand cmd = new("SELECT TOP 1 * FROM Utenti WHERE nome = @nome;");
        cmd.Parameters.AddWithValue("@nome", nome);

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
                INSERT INTO Utenti (nome, pass, ruolo)
                VALUES (@nome, HASHBYTES('SHA2_512', @pass), @ruolo); 
            ");
        cmd.Parameters.AddWithValue("@nome", utente.Nome);
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
                    pass = HASHBYTES('SHA2_512', @pass),
                    ruolo = @ruolo
                WHERE id = @id;
            ");
        cmd.Parameters.AddWithValue("@nome", utente.Nome);
        cmd.Parameters.AddWithValue("@pass", utente.Pass);
        cmd.Parameters.AddWithValue("@ruolo", utente.Ruolo);
        cmd.Parameters.AddWithValue("@id", utente.Id);

        return _db.ExecQuery(cmd);
    }

    public bool Validate(string nome, string pass)
    {
        SqlCommand cmd = new(@$"
                SELECT TOP 1 * FROM Utenti
                WHERE nome = @nome
                AND pass = HASHBYTES('SHA2_512', @pass);
            ");
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@pass", pass);

        Dictionary<string, object>? record = _db.ReadOne(cmd);
        if (record is null) return false;
        return true;
    }
}