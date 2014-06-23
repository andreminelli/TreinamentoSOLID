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
        private readonly IAccountExtractor _accountExtractor;
        private readonly IAccountLoading _accountLoader;

        public EtlProcessor(IAccountExtractor accountExtractor, IAccountLoading accountLoader)
        {
            this._accountExtractor = accountExtractor;
            this._accountLoader = accountLoader;
        }

        public void Execute()
        {
            using (this._accountExtractor)
            {
                using (this._accountLoader)
                {
                    try
                    {
                        AccountData data;
                        while ((data = this._accountExtractor.GetNext()) != null)
                        {
                            this._accountLoader.Add(data);
                        }

                        this._accountLoader.Commit();
                    }
                    catch (Exception)
                    {
                        this._accountLoader.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
