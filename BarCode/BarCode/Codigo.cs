﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BarCode
{
    public class Codigo
    {
        public string Code { get; set; }
        public string Quantity { get; set; }

        public override string ToString()
        {
            return Code;
        }

        public Codigo(string code, string quantity)
        {
            Code = code;
            Quantity = quantity;
        }
    }
}
