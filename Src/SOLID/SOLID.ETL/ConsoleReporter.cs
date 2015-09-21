using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    class ConsoleReporter : IReportAccountLoaded
    {
        private int count = 0;

        public void ReportLoaded(AccountData data)
        {
            count++;
        }

        public void LoadFinished()
        {
            Console.WriteLine("Registros inseridos: {0}", count);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }
    }
}
