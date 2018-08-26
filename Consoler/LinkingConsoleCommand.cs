using System;
using System.Collections.Generic;
using System.Text;

namespace Consoler
{
    public class LinkingConsoleCommand : AbstractConsoleCommand
    {

        public IEnumerable<AbstractConsoleCommand> Children => _children;

        readonly List<AbstractConsoleCommand> _children;

        public LinkingConsoleCommand(string Command) : base(Command)
        {
            _children = new List<AbstractConsoleCommand>();
        }

        public AbstractConsoleCommand AddCommands(params AbstractConsoleCommand[] consoleCommands)
        {
            _children.AddRange(consoleCommands);
            foreach (var c in consoleCommands) c.Parent = this;
            return this;
        }
    }
}
