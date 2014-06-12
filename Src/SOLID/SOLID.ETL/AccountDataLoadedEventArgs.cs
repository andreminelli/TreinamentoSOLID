using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public class AccountDataLoadedEventArgs : EventArgs
    {
        public AccountDataLoadedEventArgs(AccountData data)
        {
            Data = data;
        }

        public AccountData Data { get; private set; }
    }
}
