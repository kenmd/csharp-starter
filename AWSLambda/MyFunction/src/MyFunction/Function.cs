using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Amazon.Lambda.Core;
using Dapper;
using MyService;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

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
        public string FunctionHandler(string input, ILambdaContext _)
        {
            LambdaLogger.Log("Input: " + input);

            // ConnectToDatabase();

            LambdaLogger.Log($"From custom class library: IsEven(2) {SampleCalcUtil.IsEven(2)}");

            return input?.ToUpper();
        }

        private void ConnectToDatabase()
        {
            using (IDbConnection db = new SqlConnection(DBUtil.ConnString()))
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
