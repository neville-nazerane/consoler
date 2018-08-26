using Consoler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tester
{
    public class Builder : ConsoleCommand
    {
        readonly ValuedConsoleFlag config;

        public Builder() : base("build")
        {
            AddFlags(config = new ValuedConsoleFlag("-c, --configuration"));
            config.Value = "config";
        }

        public override void Run()
        {
            Console.WriteLine($"you choose to build with a {config.Value} config");
        }

    }
}
