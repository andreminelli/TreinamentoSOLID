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

            var countingValidator = new CountingValidator();
            new EtlProcessor(sourceFilePath, targetConnectionString, countingValidator).Execute();
            var total = countingValidator.Count;

            Console.WriteLine("Registros inseridos: {0}", total);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }
    }
}
