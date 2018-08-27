using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consoler
{
    public class ConsoleCommand : AbstractConsoleCommand
    {

        public ConsoleOption[] Options { get; set; }
        public ConsoleOption[] OptionalOptions { get; set; }
        public IEnumerable<BasicConsoleFlag> Flags => _flags;

        public Action OnRun { get; set; }
        public Func<Task> OnRunAsync { get; set; }

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

        public virtual void Run() => OnRun?.Invoke();

        public virtual async Task RunAsync()
        {
            if (OnRunAsync != null) await OnRunAsync();
        }
    }
}
