using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public class CountingOutput
    {
        private EtlProcessor etlProcessor;

        public CountingOutput(EtlProcessor etlProcessor)
        {
            this.etlProcessor = etlProcessor;
        }

        public void Execute()
        {
            int total = 0;
            etlProcessor.AccountLoaded += (sender, e) => { total++; };
            etlProcessor.Execute();

            Console.WriteLine("Registros inseridos: {0}", total);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }
    }
}
