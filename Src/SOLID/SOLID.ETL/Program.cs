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

            var countingRegistration = Lifestyle.Singleton.CreateRegistration<CountingAccountLoading>(
                () => new CountingAccountLoading(new SqlAccountLoading(targetConnectionString)), 
                container);
            container.AddRegistration(typeof(ICount), countingRegistration);
            container.AddRegistration(typeof(IAccountLoading), countingRegistration);
            container.Register<IAccountExtractor>(() => new CsvAccountExtractor(sourceFilePath));

            container.GetInstance<EtlProcessor>().Execute();

            Console.WriteLine("Registros inseridos: {0}", container.GetInstance<ICount>().Count);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }
    }
}
