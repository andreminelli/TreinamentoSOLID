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
        private IReportAccountLoaded _reporter;

        public EtlProcessor(string sourceFilePath, string targetConnectionString)
            : this(sourceFilePath, targetConnectionString, new NullReporter())
        { }

        public EtlProcessor(string sourceFilePath, string targetConnectionString, IReportAccountLoaded reporter)
        {
            this._sourceFilePath = sourceFilePath;
            this._targetConnectionString = targetConnectionString;
            this._reporter = reporter;
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
                            _reporter.ReportLoaded(data);
                        }

                        loader.Commit();
                        _reporter.LoadFinished();
                    }
                    catch (Exception)
                    {
                        loader.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
