using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public class CountingEtlProcessor : EtlProcessor
    {
        public CountingEtlProcessor(string sourceFilePath, string targetConnectionString)
            : base(sourceFilePath, targetConnectionString)
        {  }

        public int _recordsProcessed;

        public override void Execute()
        {
            _recordsProcessed = 0;

            base.Execute();

            Console.WriteLine("Registros inseridos: {0}", _recordsProcessed);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }

        protected override void Load(AccountData data)
        {
            base.Load(data);
            _recordsProcessed++;
        }
    }
}
