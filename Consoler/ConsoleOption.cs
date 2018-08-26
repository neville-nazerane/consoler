using System;
using System.Collections.Generic;
using System.Text;

namespace Consoler
{
    public class ConsoleOption
    {

        public ConsoleOption()
        {

        }

        public ConsoleOption(string name, string description = null, string value = null)
        {
            Name = name;
            Description = description;
            Value = value;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

    }
}
