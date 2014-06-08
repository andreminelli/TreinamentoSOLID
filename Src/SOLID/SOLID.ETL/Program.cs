using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SOLID.ETL
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = args[0];

            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine();
                var headers = line.Split(',').ToArray();

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ETL"].ConnectionString))
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();

                    try
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            var values = line.Split(',').ToArray();

                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.CommandText = "INSERT INTO Accounts (Number, Name) VALUES (@number, @name)";
                                cmd.Transaction = transaction;
                                cmd.Parameters.AddWithValue("@number", values[0]);
                                cmd.Parameters.AddWithValue("@name", values[1]);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
