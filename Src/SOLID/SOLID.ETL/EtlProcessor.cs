using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public class EtlProcessor
    {
        private string _sourceFilePath;
        private string _targetConnectionString;

        public EtlProcessor(string sourceFilePath, string targetConnectionString)
        {
            this._sourceFilePath = sourceFilePath;
            this._targetConnectionString = targetConnectionString;
        }

        public void Execute()
        {
            using (var reader = new StreamReader(_sourceFilePath))
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
