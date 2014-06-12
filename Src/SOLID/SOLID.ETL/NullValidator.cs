using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    class NullValidator : IValidateAccount
    {
        public void Validate(AccountData data)
        { }
    }
}
