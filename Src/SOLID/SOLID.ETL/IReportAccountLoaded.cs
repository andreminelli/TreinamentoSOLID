using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public interface IReportAccountLoaded
    {
        void ReportLoaded(AccountData data);
        void LoadFinished();
    }
}
