using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public class SqlAccountLoading : IDisposable
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public SqlAccountLoading(string connectionString)
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ETL"].ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Add(AccountData data)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Accounts (Number, Name) VALUES (@number, @name)";
                cmd.Transaction = _transaction;
                cmd.Parameters.AddWithValue("@number", data.Number);
                cmd.Parameters.AddWithValue("@name", data.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Dispose();
            }
        }
    }
}
