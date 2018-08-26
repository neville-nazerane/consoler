using Consoler;
using System;

namespace Tester
{
    class Program
    {

        static void Main(string[] args)
        {

            string command = "new mvc -o hello";
            command = "build -c release";

            var _new = new LinkedConsoleCommand("new")
                            .AddCommands(new Mvc(), new ConsoleLib());

            var _add = new LinkedConsoleCommand("add")
                            {
                                Description = "Add something new"
                            }
                            .AddCommands(new Package());

            var control = new ConsoleControl();
            control.AddCommand(_new, _add, new Builder()); // adding root commands
            control.Compute(args);

            while (true)
            {
                control.Compute(command.Trim().Split(" "));
                command = Console.ReadLine();
            }

        }

    }
}
