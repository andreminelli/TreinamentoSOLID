using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public class CountingSqlAccountLoader : SqlAccountLoader
    {
        private string _targetConnectionString;

        public CountingSqlAccountLoader(string _targetConnectionString)
            : base(_targetConnectionString)
        {
            RecordsLoaded = 0;
        }

        public int RecordsLoaded { get; private set; }

        public override void Add(AccountData data)
        {
            base.Add(data);
            RecordsLoaded++;
        }
    }
}
