using System;
using System.Collections.Generic;
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
            using (var extractor = new AccountExtraction(_sourceFilePath))
            {
                using (var connection = new SqlConnection(_targetConnectionString))
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
