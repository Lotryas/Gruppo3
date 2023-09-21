using System.Data.SqlClient;
using Utility;

namespace Libreria.Models
{
    public class DAOLibro : IDAO
    {
        private static Database? _db;
        private static DAOLibro? _instance = null;

        public static DAOLibro GetInstance()
        {
            return _instance ??= new DAOLibro();
        }

        private DAOLibro()
        {
            _db = new(Config.ConnectionString.Value);
        }

        public bool Delete(long id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Libri WHERE id = @id;");
            cmd.Parameters.AddWithValue("@id",id);
            return _db.ExecQuery(cmd);
        }

        public Entity? Find(long id)
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Libri WHERE id = @id;");
            cmd.Parameters.AddWithValue("@id", id);
            Dictionary<string, object>? record = _db.ReadOne(cmd);
            if (record is null)
                return null;
            Libro l = new();
            l.PopulateFromRecord(record);
            return l;
            
        }

        public bool Insert(Entity entity)
        {
            Libro l = (Libro)entity;
            SqlCommand cmd = new SqlCommand("INSERT INTO Libri\n" +
                "(titolo, autore, genere, quantita, formato, nomeFile)\n" +
                "VALUES\n" +
                @"(@titolo, @autore, @genere, @quantita, @formato, @nomeFile)");
            cmd.Parameters.AddWithValue("@titolo", l.Titolo);
            cmd.Parameters.AddWithValue("@autore", l.Autore);
            cmd.Parameters.AddWithValue("@genere", l.Genere);
            cmd.Parameters.AddWithValue("@quantita", l.Quantita);
            cmd.Parameters.AddWithValue("@formato", l.Formato);
            cmd.Parameters.AddWithValue("@nomeFile", l.NomeFile);
            return _db.ExecQuery(cmd);
        }

        public List<Entity> ReadAll()
        {
            List<Entity> ris = new();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Libri;");
            List<Dictionary<string, object>> tabella = _db.ReadMany(cmd);
            foreach (Dictionary<string,object> riga in tabella)
            {
                Libro l = new();
                l.PopulateFromRecord(riga);
                ris.Add(l);
            }
            return ris;
        }

        public bool Update(Entity entity)
        {
            Libro l = (Libro)entity;
            SqlCommand cmd = new SqlCommand("UPDATE INTO Libri\n" +
                "SET\n" +
                @"titolo = @titolo,\n" +
                @"autore = @autore,\n" +
                @"genere = @genere,\n" +
                @"quantita = @quantita,\n" +
                @"formato = @formato,\n" +
                @"nomeFile = @nomeFile\n" +
                @"WHERE id = @id;");
            cmd.Parameters.AddWithValue("@id", l.Id);
            cmd.Parameters.AddWithValue("@titolo", l.Titolo);
            cmd.Parameters.AddWithValue("@autore", l.Autore);
            cmd.Parameters.AddWithValue("@genere", l.Genere);
            cmd.Parameters.AddWithValue("@quantita", l.Quantita);
            cmd.Parameters.AddWithValue("@formato", l.Formato);
            cmd.Parameters.AddWithValue("@nomeFile", l.NomeFile);
            return _db.ExecQuery(cmd);
        }

        public List<Entity> FindAutore(string autore)
        {
            List<Entity> ris = new();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Libri WHERE autore = @autore");
            cmd.Parameters.AddWithValue("@autore", autore);
            List<Dictionary<string, object>> tabella = _db.ReadMany(cmd);
            if (tabella is null)
                return null;
            foreach (Dictionary<string, object> riga in tabella)
            {
                Libro l = new();
                l.PopulateFromRecord(riga);
                ris.Add(l);
            }
            return ris;
        }

        public List<Entity> FindTitolo(string titolo)
        {
            List<Entity> ris = new();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Libri WHERE titolo LIKE '%' + @titolo + '%';");
            cmd.Parameters.AddWithValue("@titolo", titolo);
            List<Dictionary<string, object>> tabella = _db.ReadMany(cmd);
            if (tabella is null)
                return null;
            foreach (Dictionary<string, object> riga in tabella)
            {
                Libro l = new();
                l.PopulateFromRecord(riga);
                ris.Add(l);
            }
            return ris;
        }
    }
}
