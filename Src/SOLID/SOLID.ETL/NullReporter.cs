using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    class NullReporter : IReportAccountLoaded
    {
        public void ReportLoaded(AccountData data)
        { }


        public void LoadFinished()
        { }
    }
}
