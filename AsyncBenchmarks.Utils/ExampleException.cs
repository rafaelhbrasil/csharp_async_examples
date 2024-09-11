using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.Utils
{
    public class ExampleException: Exception
    {
        public ExampleException(string message): base(message)
        {
        }
    }
}
