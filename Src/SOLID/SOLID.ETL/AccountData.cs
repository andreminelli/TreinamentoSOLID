using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public class AccountData
    {
        public static AccountData Empty = new AccountData { Number = string.Empty, Name = string.Empty };

        public string Number { get; set; }

        public string Name { get; set; }
    }
}
