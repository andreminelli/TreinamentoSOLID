using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public class CountingEtlProcessor : EtlProcessor
    {
        private CountingSqlAccountLoader _accountLoader;

        public CountingEtlProcessor(string sourceFilePath, string targetConnectionString)
            : base(sourceFilePath, targetConnectionString)
        {  }

        public int RecordsProcessed { get; private set; }

        public override void Execute()
        {
            base.Execute();
            RecordsProcessed = _accountLoader.RecordsLoaded;
        }

        protected override SqlAccountLoader MakeAccountLoader()
        {
            _accountLoader = new CountingSqlAccountLoader(_targetConnectionString);
            return _accountLoader;
        }
    }
}
