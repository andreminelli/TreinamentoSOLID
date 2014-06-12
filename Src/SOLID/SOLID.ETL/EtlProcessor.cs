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

        public event EventHandler<AccountDataLoadedEventArgs> AccountLoaded;

        public EtlProcessor(string sourceFilePath, string targetConnectionString)
        {
            this._sourceFilePath = sourceFilePath;
            this._targetConnectionString = targetConnectionString;
        }

        public void Execute()
        {
            using (var extractor = new CsvAccountExtractor(_sourceFilePath))
            {
                using (var loader = new SqlAccountLoading(_targetConnectionString))
                {
                    try
                    {
                        AccountData data;
                        while ((data = extractor.GetNext()) != null)
                        {
                            loader.Add(data);
                            OnAccountLoaded(data);
                        }

                        loader.Commit();
                    }
                    catch (Exception)
                    {
                        loader.Rollback();
                        throw;
                    }
                }
            }
        }

        protected virtual void OnAccountLoaded(AccountData data)
        {
            if (AccountLoaded != null)
            {
                AccountLoaded(this, new AccountDataLoadedEventArgs(data));
            }
        }
    }
}
