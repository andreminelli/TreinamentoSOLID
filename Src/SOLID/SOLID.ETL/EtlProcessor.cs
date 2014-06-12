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

        public EtlProcessor(string sourceFilePath, string targetConnectionString)
        {
            this._sourceFilePath = sourceFilePath;
            this._targetConnectionString = targetConnectionString;
        }

        public virtual void Execute()
        {
            using (var extractor = MakeAccountExtractor())
            {
                using (var loader = MakeAccountLoader())
                {
                    try
                    {
                        AccountData data;
                        while ((data = extractor.GetNext()) != null)
                        {
                            loader.Add(data);
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

        protected virtual SqlAccountLoader MakeAccountLoader()
        {
            return new SqlAccountLoader(_targetConnectionString);
        }

        protected virtual CsvAccountExtractor MakeAccountExtractor()
        {
            return new CsvAccountExtractor(_sourceFilePath);
        }
    }
}
