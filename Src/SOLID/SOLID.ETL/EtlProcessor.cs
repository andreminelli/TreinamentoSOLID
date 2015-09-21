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
        protected string _sourceFilePath;
        protected string _targetConnectionString;

        private SqlAccountLoader _loader;

        public EtlProcessor(string sourceFilePath, string targetConnectionString)
        {
            this._sourceFilePath = sourceFilePath;
            this._targetConnectionString = targetConnectionString;
        }

        public virtual void Execute()
        {
            using (var extractor = new CsvAccountExtractor(_sourceFilePath))
            {
                using (_loader = new SqlAccountLoader(_targetConnectionString))
                {
                    try
                    {
                        AccountData data;
                        while ((data = extractor.GetNext()) != null)
                        {
                            Load(data);
                        }

                        _loader.Commit();
                    }
                    catch (Exception)
                    {
                        _loader.Rollback();
                        throw;
                    }
                }
            }
        }

        protected virtual void Load(AccountData data)
        {
            _loader.Add(data);
        }
    }
}
