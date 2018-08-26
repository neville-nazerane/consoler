using System;
using System.Collections.Generic;
using System.Text;

namespace Consoler
{
    public abstract class AbstractConsoleCommand
    {

        public virtual string Command { get; }
        public virtual string Description { get; set; }

        public AbstractConsoleCommand Parent { get; set; }

        public AbstractConsoleCommand(string Command)
        {
            this.Command = Command;
        }

    }
}
