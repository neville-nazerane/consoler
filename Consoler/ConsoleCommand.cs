using System;
using System.Collections.Generic;

namespace Consoler
{
    public class ConsoleCommand : AbstractConsoleCommand
    {

        public ConsoleOption[] Options { get; protected set; }
        public ConsoleOption[] OptionalOptions { get; protected set; }
        public IEnumerable<BasicConsoleFlag> Flags => _flags;

        public Action OnRun { get; set; }

        readonly List<BasicConsoleFlag> _flags;

        public ConsoleCommand(string Command) : base(Command)
        {
            _flags = new List<BasicConsoleFlag>();
        }

        public AbstractConsoleCommand AddFlags(params BasicConsoleFlag[] flags)
        {
            _flags.AddRange(flags);
            return this;
        }

        public virtual void Run()
        {
            OnRun();
        }

    }
}
