using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using IntroLibrary;

namespace IntroUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();

            using (IDbConnection db = new SqlConnection(Helper.Conn("SQLServer")))
            {
                string query = "SELECT * FROM PERSON";
                people = db.Query<Person>(query).ToList();

                foreach (var p in people)
                {
                    Console.WriteLine($"{p.ID}: {p.NAME}");
                }
            }

            // Linq examples
            // https://qiita.com/nskydiving/items/c9c47c1e48ea365f8995
            var list = new List<int> { 1, 84, 95, 95, 40, 6 };

            list.FindAll(x => x % 2 == 0);
            list.ConvertAll(x => x * 3);

            list.First();
            list.Last();
            list.Max();

            int[] array = list.ToArray();
            List<object> objects = list.Cast<object>().ToList();

            var numbers = from x in list
                          where x % 2 == 0
                          orderby x
                          select x * 3;

            foreach (var x in numbers)
            {
                Console.WriteLine(x);
            }
        }
    }
}
