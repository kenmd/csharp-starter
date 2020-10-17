using Microsoft.Data.SqlClient;
using static System.Environment;

namespace MyFunction
{
    public static class DBUtil
    {
        public static string ConnString()
        {
            var builder = new SqlConnectionStringBuilder() {
                DataSource = GetEnvironmentVariable("DB_SERVER"),
                InitialCatalog = GetEnvironmentVariable("DB_DATABASE"),
                UserID = GetEnvironmentVariable("DB_USERNAME"),
                Password = GetEnvironmentVariable("DB_PASSWORD"),
            };

            return builder.ConnectionString;
        }
    }
}
