using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    class CountingValidator : IValidateAccount
    {
        public void Validate(AccountData data)
        {
            Count++;
        }

        public int Count { get; private set; }
    }
}
