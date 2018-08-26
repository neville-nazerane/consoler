using Consoler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tester
{
    public class Package : ConsoleCommand
    {

        string _Description;
        readonly ConsoleOption PackageName;
        readonly ConsoleOption MakeWith;
        readonly ConsoleOption Opto;

        public override string Description {
            get
            {
                if (_Description != null)
                    return _Description;
                switch (Parent.Command)
                {
                    case "add":
                        return "add a new nuget package";
                    default:
                        return "a nuget package";
                }
            }
            set => _Description = value; }

        public Package() : base("package")
        {

            Options = new ConsoleOption[] {
                PackageName = new ConsoleOption("package name", "Name of the nuget package"),
                MakeWith = new ConsoleOption("make with")
            };

            OptionalOptions = new ConsoleOption[] {
                Opto = new ConsoleOption("Optional tester")
            };

        }

        public override void Run()
        {
            Console.WriteLine($"You have created a package with name {PackageName.Value} made with {MakeWith.Value}");
            if (Opto.Value != null)
                Console.WriteLine($"you bothered to use the optional {Opto.Value}!!!");
        }

    }
}
