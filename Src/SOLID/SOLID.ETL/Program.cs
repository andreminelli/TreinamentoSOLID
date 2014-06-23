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
            var sourceFilePath = args[0];
            var targetConnectionString = ConfigurationManager.ConnectionStrings["ETL"].ConnectionString;

            var accountExtractor = new CsvAccountExtractor(sourceFilePath);
            var accountLoader = new CountingAccountLoading(new SqlAccountLoading(targetConnectionString));
            new EtlProcessor(accountExtractor, accountLoader).Execute();

            Console.WriteLine("Registros inseridos: {0}", accountLoader.Count);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }
    }
}
