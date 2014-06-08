using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public class AccountExtraction : IDisposable
    {
        private StreamReader _reader;
        private string[] _headers;

        public AccountExtraction(string filePath, char separator = ',')
        {
            _reader = new StreamReader(filePath);

            var line = _reader.ReadLine();
            _headers = line.Split(separator).ToArray();
        }

        public AccountData GetNext()
        {
            var line = _reader.ReadLine();

            if (line != null)
            {
                var values = line.Split(',').ToArray();
                return new AccountData { Number = values[0], Name = values[1] };
            }

            return null;
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }
        }
    }
}
