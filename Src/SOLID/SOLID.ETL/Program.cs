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

            var total = 0;
            var processor = new EtlProcessor(sourceFilePath, targetConnectionString);

            processor.AccountLoaded += (sender, e) => { total++; };                
            processor.Execute();

            Console.WriteLine("Registros inseridos: {0}", total);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }
    }
}
