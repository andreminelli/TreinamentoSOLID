using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = args[0];

            var connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=ETL.mdf;Integrated Security=True;Connect Timeout=30");

            var data = new List<Dictionary<string, string>>();

            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine();
                var headers = line.Split(',').ToArray();

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',').ToArray();
                    var lineData = new Dictionary<string, string>();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        lineData.Add(headers[i], values[i]);

                        var cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO Account (Number, Name) VALUES (@number, @name)";
                        cmd.Parameters.AddWithValue("@number", values[0]);
                        cmd.Parameters.AddWithValue("@name", values[1]);
                        cmd.ExecuteNonQuery();
                    }

                    data.Add(lineData);
                }
            }

            foreach (var dataItem in data)
            { 

            }


        }
    }
}
