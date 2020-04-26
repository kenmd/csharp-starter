using System;

namespace MyFunction
{
    public static class Util
    {
        public static string ConnString()
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string database = Environment.GetEnvironmentVariable("DB_DATABASE");
            string username = Environment.GetEnvironmentVariable("DB_USERNAME");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            string str = "Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
            string ConnString = String.Format(str, server, database, username, password);

            return ConnString;
        }
    }
}
