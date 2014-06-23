using SimpleInjector;
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

            container.Register<IAccountLoading>(() => new SqlAccountLoading(targetConnectionString));
            container.Register<IAccountExtractor>(() => new CsvAccountExtractor(sourceFilePath));

            container.InterceptWith<CountingLoadingInterceptor>(type => type.Name.EndsWith("Loading"));

            container.GetInstance<EtlProcessor>().Execute();
        }
    }
}
