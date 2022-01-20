using System;
using System.Collections.Generic;
using System.Text;

namespace BarCode
{
    class OCode
    {
        public string Code { get; set; }
        public string Machine { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Code;
        }

        public OCode(string code, string name, string machin)
        {
            Code = code;
            Name = name;
            Machine = machin;
        }
    }
}
