using System.Data.SqlClient;

namespace Utility
{
    public class Database
    {
        private readonly SqlConnection _conn;

        // Use this if you want to use the Config.txt connection string
        public Database(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
        }

        // Use this when on a Windows system!
        public Database(string dbName, string dbServer)
        {
            string connectionString =
                $"Data Source={dbServer};" +
                $"Initial Catalog={dbName};" +
                "Integrated Security=True;";

            _conn = new SqlConnection(connectionString);
        }

        // Use this when on a Linux system!
        public Database(string dbName, string dbServer, string dbUser, string dbPassword)
        {
            string connectionString =
                $"Server={dbServer};" +
                $"Database={dbName};" +
                $"User Id={dbUser};" +
                $"Password={dbPassword};";

            _conn = new SqlConnection(connectionString);
        }

        // Using SqlCommand parameter to avoid SQL Injection!
        public bool ExecQuery(SqlCommand cmd)
        {
            int rowsAffected = 0;

            cmd.Connection = _conn;

            try
            {
                _conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR QUERY: {cmd.CommandText}");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return rowsAffected > 0;
        }

        // Using SqlCommand parameter to avoid SQL Injection!
        public Dictionary<string, object>? ReadOne(SqlCommand cmd)
        {
            Dictionary<string, object> record = new();

            SqlDataReader? dr = null;

            cmd.Connection = _conn;

            try
            {
                _conn.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        record.Add(dr.GetName(i), dr.GetValue(i));
                    }
                }
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR QUERY: {cmd.CommandText}");
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                dr?.Close();
                _conn.Close();
            }

            return record;
        }

        // Using SqlCommand parameter to avoid SQL Injection!
        public List<Dictionary<string, object>> ReadMany(SqlCommand cmd)
        {
            List<Dictionary<string, object>> records = new();

            SqlDataReader? dr = null;

            cmd.Connection = _conn;

            try
            {
                _conn.Open();
                dr = cmd.ExecuteReader();

                // Iterates over every record in the result
                while (dr.Read())
                {
                    Dictionary<string, object> record = new();

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        record.Add(dr.GetName(i), dr.GetValue(i));
                    }

                    records.Add(record);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR QUERY: {cmd.CommandText}");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dr?.Close();
                _conn.Close();
            }

            return records;
        }
    }
}