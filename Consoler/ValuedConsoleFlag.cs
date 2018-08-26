using System;
using System.Collections.Generic;
using System.Text;

namespace Consoler
{
    public class ValuedConsoleFlag : BasicConsoleFlag
    {

        public string Value { get; set; }

        public bool IsOptionalValue { get; set; }

        public ValuedConsoleFlag(string[] names, bool IsOptionalValue = false) : base(names)
        {
            this.IsOptionalValue = IsOptionalValue;
        }

        public ValuedConsoleFlag(string names, bool IsOptionalValue = false) : base(names)
        {
            this.IsOptionalValue = IsOptionalValue;
        }

        public static implicit operator string(ValuedConsoleFlag flag)
            => flag.Value;

    }
}
