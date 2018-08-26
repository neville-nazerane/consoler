using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Consoler
{
    public class ConsoleControl
    {

        readonly List<AbstractConsoleCommand> _commands;

        IEnumerable<BasicConsoleFlag> Flags => _flags;

        public bool AllowEmpty { get; set; }

        readonly List<BasicConsoleFlag> _flags;
        
        public ConsoleControl()
        {
            _commands = new List<AbstractConsoleCommand>();
            _flags = new List<BasicConsoleFlag>();
        }

        public void AddFlags(params BasicConsoleFlag[] flags)
        {
            _flags.AddRange(flags);
        }

        public void AddCommand(params AbstractConsoleCommand[] command) 
            => _commands.AddRange(command);

        public bool Compute(string[] args)
        {
            if (args?.Count() > 0)
            {
                IEnumerable<AbstractConsoleCommand> commands = _commands;

                if (args[0].StartsWith("-")) ComputeFlags(Flags, args, 0);
                else // must contain commands
                {
                    int i = 0;
                    AbstractConsoleCommand command = null, lastValidCommand;
                    do
                    {
                        lastValidCommand = command;
                        command = commands.SingleOrDefault(c => c.Command == args[i]);
                        if (command is LinkingConsoleCommand linked)
                            commands = linked.Children;
                        i++;
                    } while (command != null
                                && i < args.Count()
                                && command is LinkingConsoleCommand);

                    if (command != null && command is ConsoleCommand ending)
                    {
                        if (ending.Options?.Count() > 0)
                        {
                            foreach (var opt in ending.Options)
                            {
                                if (i == args.Count())
                                {
                                    var foreGround = Console.ForegroundColor;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid options");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("The options need to be: " +
                                            string.Join(" ", 
                                                ending.Options.Select(o => $"<{o.Name}>")
                                                    .Union(ending.OptionalOptions.Select(o => $"[{o.Name}]"))
                                                ));
                                    Console.ForegroundColor = foreGround;
                                    return false;
                                }
                                opt.Value = args[i++];
                            }
                        }
                        if (ending.OptionalOptions?.Count() > 0)
                        {
                            foreach (var opt in ending.OptionalOptions)
                            {
                                if (i == args.Count() || args[i].StartsWith("-")) break;
                                opt.Value = args[i++];
                            }
                        }
                        if (!ComputeFlags(ending.Flags, args, i)) return false;
                        ending.Run();
                    }
                    else
                    {
                        var foreGround = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unable to find command");
                        if (command != null && command is LinkingConsoleCommand linked)
                            PrintCommands(linked.Children, $"Valid commands for '{linked.Command}' are: ");
                        else PrintCommands(_commands);
                        Console.ForegroundColor = foreGround;
                        return false;
                    }
                }
            }
            else if (!AllowEmpty)
            {
                var foreGround = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No command sent");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (_commands?.Count() > 0)
                {
                    Console.WriteLine("Valid commands are: " +
                                        string.Join(", ", _commands.Select(c => c.Command)));
                    foreach (var c in _commands)
                        if (c.Description != null)
                            Console.WriteLine($"{c.Command}: {c.Description}");
                }

                Console.ForegroundColor = foreGround;
            }
            return true;
        }

        void PrintCommands(IEnumerable<AbstractConsoleCommand> commands, string preText = "Valid commands are: ")
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(preText +
                            string.Join(", ", commands.Select(c => c.Command)));
            foreach (var cmd in commands)
                if (!string.IsNullOrWhiteSpace(cmd.Description))
                    Console.WriteLine($"{cmd.Command}: {cmd.Description}");
        }

        bool ComputeFlags(IEnumerable<BasicConsoleFlag> flags, string[] args, int fromIndex)
        {
           int i;
            for (i = fromIndex; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("-"))
                {
                    var flag = flags.SingleOrDefault(f => f.names.Contains(arg));
                    if (flag == null)
                    {
                        var foreGround = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid flag: {arg}");
                        Console.ForegroundColor = foreGround;
                        return false;
                    }
                    flag.IsUsed = true;
  
                    if (flag is ValuedConsoleFlag valued)
                    {
                        bool noNext = i + 1 == args.Length || args[i + 1].StartsWith("-");
                        if (noNext && !valued.IsOptionalValue)
                        {
                            var foreGround = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("invalid value for " + string.Join(", ", flag.names));
                            Console.ForegroundColor = foreGround;
                            return false;
                        }
                        else if (!noNext) valued.Value = args[++i];
                    }
                }
                else
                {
                    var foreGround = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid flag: " + arg);
                    Console.ForegroundColor = foreGround;
                    return false;
                }
            }
            return true;
        }

    }
}
