using System.Configuration;

namespace IntroUI
{
    public static class Helper
    {
        public static string Conn(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
