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
                MakeWith = new ConsoleOption()
            };

        }

        public override void Run()
        {
            Console.WriteLine("You have created a package with name " + PackageName.Value);
        }

    }
}
