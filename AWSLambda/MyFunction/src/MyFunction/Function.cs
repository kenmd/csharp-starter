using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Amazon.Lambda.Core;
using Dapper;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]

namespace MyFunction
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            LambdaLogger.Log("Input: " + input);

            ConnectToDatabase();

            return input?.ToUpper();
        }

        private void ConnectToDatabase()
        {
            using (IDbConnection db = new SqlConnection(Util.ConnString()))
            {
                db.Open();
                db.ChangeDatabase("db_example");

                var person = db.Get<Person>(2);

                LambdaLogger.Log($"Person {person.ID}: {person.NAME}");

                var people = db.GetList<Person>();

                foreach (var p in people)
                {
                    LambdaLogger.Log($"{p.ID}: {p.NAME}");
                }
            }
        }
    }
}
