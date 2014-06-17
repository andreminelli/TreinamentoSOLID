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
        private IValidateAccount _validator;

        public EtlProcessor(string sourceFilePath, string targetConnectionString, IValidateAccount validator)
        {
            this._sourceFilePath = sourceFilePath;
            this._targetConnectionString = targetConnectionString;
            this._validator = validator ?? new NullValidator();
        }

        public void Execute()
        {
            using (var extractor = new DerivedCsvAccountExtractor(_sourceFilePath))
            {
                using (var loader = new SqlAccountLoading(_targetConnectionString))
                {
                    try
                    {
                        AccountData data;
                        while ((data = extractor.GetNext()) != AccountData.Empty)
                        {
                            _validator.Validate(data);
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
    }
}
