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
                using (var loader = new AccountLoading(ConfigurationManager.ConnectionStrings["ETL"].ConnectionString))
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
    }
}
