using System.Data.SqlClient;
using Utility;

namespace Libreria.Models;

public class DAOLibro : IDAO
{
    private readonly Database _db;
    private static DAOLibro? _instance;

    private DAOLibro()
    {
        _db = new(Config.ConnectionString.Value);
    }

    public static DAOLibro GetInstance()
    {
        return _instance ??= new DAOLibro();
    }

    private List<Entity> ReadManyLibri(SqlCommand cmd)
    {
        List<Dictionary<string, object>> tabella = _db.ReadMany(cmd);

        List<Entity> libri = new();
        foreach (Dictionary<string, object> riga in tabella)
        {
            Libro l = new();
            l.PopulateFromRecord(riga);
            libri.Add(l);
        }

        return libri;
    }

    public bool Delete(long id)
    {
        SqlCommand cmd = new("DELETE FROM Libri WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);
        return _db.ExecQuery(cmd);
    }

    public Entity? Find(long id)
    {
        SqlCommand cmd = new("SELECT TOP 1 * FROM Libri WHERE id = @id;");
        cmd.Parameters.AddWithValue("@id", id);

        Dictionary<string, object>? record = _db.ReadOne(cmd);
        if (record is null) return null;

        Libro l = new();
        l.PopulateFromRecord(record);
        return l;
    }

    public bool Insert(Entity entity)
    {
        Libro l = (Libro)entity;

        SqlCommand cmd = new(@"
            INSERT INTO Libri (titolo, autore, genere, quantita, formato, nomeFile, locandina)
            VALUES (@titolo, @autore, @genere, @quantita, @formato, @nomeFile, @locandina);
        ");
        cmd.Parameters.AddWithValue("@titolo", l.Titolo);
        cmd.Parameters.AddWithValue("@autore", l.Autore);
        cmd.Parameters.AddWithValue("@genere", l.Genere);
        cmd.Parameters.AddWithValue("@quantita", l.Quantita);
        cmd.Parameters.AddWithValue("@formato", l.Formato);
        cmd.Parameters.AddWithValue("@nomeFile", l.NomeFile);
        cmd.Parameters.AddWithValue("@locandina", l.Locandina);

        return _db.ExecQuery(cmd);
    }

    public List<Entity> ReadAll()
    {
        SqlCommand cmd = new("SELECT * FROM Libri;");
        return this.ReadManyLibri(cmd);
    }

    public bool Update(Entity entity)
    {
        Libro l = (Libro)entity;

        SqlCommand cmd = new(@"
            UPDATE Libri SET
                titolo = @titolo,
                autore = @autore,
                genere = @genere,
                quantita = @quantita,
                formato = @formato,
                nomeFile = @nomeFile,
                locandina = @locandina
            WHERE id = @id;
        ");
        cmd.Parameters.AddWithValue("@id", l.Id);
        cmd.Parameters.AddWithValue("@titolo", l.Titolo);
        cmd.Parameters.AddWithValue("@autore", l.Autore);
        cmd.Parameters.AddWithValue("@genere", l.Genere);
        cmd.Parameters.AddWithValue("@quantita", l.Quantita);
        cmd.Parameters.AddWithValue("@formato", l.Formato);
        cmd.Parameters.AddWithValue("@nomeFile", l.NomeFile);
        cmd.Parameters.AddWithValue("@locandina", l.Locandina);
        return _db.ExecQuery(cmd);
    }

    public List<Entity> FindAutore(string autore)
    {
        SqlCommand cmd = new("SELECT * FROM Libri WHERE autore = @autore;");
        cmd.Parameters.AddWithValue("@autore", autore);
        return this.ReadManyLibri(cmd);
    }

    public List<Entity> FindTitolo(string titolo)
    {
        string[] keywords = titolo.Split(' ');
        string query = "SELECT * FROM Libri WHERE ";

        for (int i = 0; i < keywords.Length; i++)
        {
            query += "(titolo LIKE @titolo" + i + " OR genere LIKE @titolo" + i + " OR autore LIKE @titolo" + i + ")";
            if (i < keywords.Length - 1)
            {
                query += " OR ";
            }
        }

        SqlCommand cmd = new SqlCommand(query);

        for (int i = 0; i < keywords.Length; i++)
        {
            cmd.Parameters.AddWithValue("@titolo" + i, "%" + keywords[i] + "%");
        }

        return this.ReadManyLibri(cmd);
    }
}
