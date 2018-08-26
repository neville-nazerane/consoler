using Consoler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tester
{
    public class ConsoleLib : ConsoleCommand
    {
        readonly ValuedConsoleFlag output;
        readonly BasicConsoleFlag slowly;

        public ConsoleLib() : base("console")
        {
            AddFlags(output = new ValuedConsoleFlag("-o, --output", true));
            AddFlags(slowly = new BasicConsoleFlag("-s, --slow"));
        }

        public override void Run()
        {
            Console.WriteLine($"new console app created on " + output?.Value ?? "current dir");
            if (slowly.IsUsed)
                Console.WriteLine("Also slowly :)");
        }

    }
}
