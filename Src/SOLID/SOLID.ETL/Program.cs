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

            using (var extractor = new AccountExtraction(args[0]))
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ETL"].ConnectionString))
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();

                    try
                    {
                        AccountData data;
                        while ((data = extractor.GetNext()) != null)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.CommandText = "INSERT INTO Accounts (Number, Name) VALUES (@number, @name)";
                                cmd.Transaction = transaction;
                                cmd.Parameters.AddWithValue("@number", data.Number);
                                cmd.Parameters.AddWithValue("@name", data.Name);
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
