using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public static class EtlProcessorExtensions
    {
        public static void ExecuteAndCount(this EtlProcessor processor)
        {
            var count = 0;

            processor.ValidateBeforeLoad = data =>
                {
                    count++;
                };

            processor.Execute();

            Console.WriteLine("Registros inseridos: {0}", count);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }

    }
}
