using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bai_tap
{
    internal class FormatException : Exception
    {
        public FormatException(string message) : base(message) { }
    }
}
