using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public class DerivedCsvAccountExtractor : CsvAccountExtractor
    {
        public DerivedCsvAccountExtractor(string filePath, char separator = ',')
            : base(filePath, separator)
        {
        }

        public override AccountData GetNext()
        {
            var result = base.GetNext();

            if (result == AccountData.Empty)
            {
                return null;
                //throw new EndOfStreamException();
            }
            else
            {
                return result;
            }
        }
    }
}
