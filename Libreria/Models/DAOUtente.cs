using System.Data.SqlClient;
using Utility;

namespace Libreria.Models;

public class DAOUtente
{
    private readonly Database _db;
    private static DAOUtente? _instance;

    private DAOUtente()
    {
        _db = new Database(
            dbName: "login_example",
            dbServer: "localhost,1433",
            dbUser: "sa",
            dbPassword: "SQLServerDevPassword!"
        );
    }

    public static DAOUtente GetInstance()
    {
        return _instance ??= new DAOUtente();
    }

    public bool Delete(long id)
    {
        SqlCommand cmd = new("DELETE FROM users WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);
        return _db.ExecQuery(cmd);
    }

    public Entity? Find(long id)
    {
        SqlCommand cmd = new("SELECT TOP 1 * FROM users WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);

        Dictionary<string, object>? record = _db.ReadOne(cmd);
        if (record is null) return null;

        Utente utente = new();
        utente.PopulateFromRecord(record);

        return utente;
    }

    public Entity? Find(string username)
    {
        SqlCommand cmd = new("SELECT TOP 1 * FROM users WHERE username = @username;");
        cmd.Parameters.AddWithValue("@username", username);

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
        cmd.Parameters.AddWithValue("@ruolo", utente.Ruolo);

        return _db.ExecQuery(cmd);
    }

    public List<Entity> ReadAll()
    {
        SqlCommand cmd = new("SELECT username FROM users ORDER BY id DESC;");
        List<Dictionary<string, object>> records = _db.ReadMany(cmd);

        List<Entity> users = new();
        foreach (Dictionary<string, object> record in records)
        {
            User user = new();
            user.PopulateFromRecord(record);
            users.Add(user);
        }

        return users;
    }

    public bool Update(Entity entity)
    {
        User user = (User)entity;

        SqlCommand cmd = new(@"
                UPDATE users SET
                    username = @username,
                    password_hash = HASHBYTES('SHA2_512', @password_hash),
                    permission_level = @permission_level
                WHERE id = @id;
            ");
        cmd.Parameters.AddWithValue("@username", user.Username);
        cmd.Parameters.AddWithValue("@password_hash", user.PasswordHash);
        cmd.Parameters.AddWithValue("@permission_level", user.PermissionLevel);
        cmd.Parameters.AddWithValue("@id", user.Id);

        return _db.ExecQuery(cmd);
    }

    public bool Validate(string username, string password)
    {
        SqlCommand cmd = new(@$"
                SELECT TOP 1 * FROM users
                WHERE username = @username
                AND password_hash = HASHBYTES('SHA2_512', @password_hash);
            ");
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password_hash", password);

        Dictionary<string, object>? record = _db.ReadOne(cmd);
        if (record is null) return false;
        return true;
    }
}