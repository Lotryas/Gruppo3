namespace Config
{
    public static class ConnectionString
    {
        public static string Value { get; private set; } = "";

        public static void SetConnectionString(string connectionString)
        {
            Value = connectionString;
        }
    }
}