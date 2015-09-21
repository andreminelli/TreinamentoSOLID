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
            container.RegisterDecorator(typeof(IAccountLoading), typeof(CountingAccountLoading));
            container.Register<IAccountExtractor>(() => new CsvAccountExtractor(sourceFilePath));
            container.Register<IAccountLoading>(() => new SqlAccountLoading(targetConnectionString));

            container.GetInstance<EtlProcessor>().Execute();
        }
    }
}
