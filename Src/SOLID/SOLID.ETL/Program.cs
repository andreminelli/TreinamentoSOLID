using SimpleInjector;
using SimpleInjector.Extensions;
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

            var container = new Container();
            container.RegisterSingleDecorator(typeof(IAccountLoading), typeof(CountingAccountLoading));
            container.Register<IAccountExtractor>(() => new CsvAccountExtractor(sourceFilePath));
            container.RegisterSingle<IAccountLoading>(() => new SqlAccountLoading(targetConnectionString));

            container.GetInstance<EtlProcessor>().Execute();

            Console.WriteLine("Registros inseridos: {0}", (container.GetInstance<IAccountLoading>() as CountingAccountLoading).Count);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }
    }
}
