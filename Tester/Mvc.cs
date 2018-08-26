using Consoler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tester
{
    class Mvc : ConsoleCommand
    {

        string Output => _o;

        readonly ValuedConsoleFlag _o;

        public Mvc() : base("mvc")
        {
            Description = "A model view controller project";
            AddFlags(_o = new ValuedConsoleFlag("-o, --output"));
        }

        public override void Run()
        {
            Console.WriteLine($"you choose to create mvc within {Output}");
        }

    }
}
