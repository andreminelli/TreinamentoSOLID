using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public static class EtlProcessorExtensions
    {
        public static int ExecuteAndCount(this EtlProcessor processor)
        {
            var count = 0;

            processor.ValidateBeforeLoad = data =>
                {
                    count++;
                    return data;
                };

            processor.Execute();

            return count;
        }

    }
}
