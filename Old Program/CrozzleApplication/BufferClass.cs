using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrozzleApplication
{
    class BufferClass
    {
        public static string Identifier { get; set; } = "";
        public static string Concatenation(string line)
        {
            line = line.Trim();
            if (Identifier!="")
            {
                line = Identifier + "-" + line;
                return line;
            }
            return line;
        }
    }
}
