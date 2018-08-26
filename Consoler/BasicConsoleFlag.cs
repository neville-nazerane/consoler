

namespace Consoler
{
    public class BasicConsoleFlag
    {

        public bool IsUsed { get; set; }


        public readonly string[] names;

        public BasicConsoleFlag(string names)
        { 
            this.names = names.Replace(", ", ",").Split(',');
        }

        public BasicConsoleFlag(string[] names)
        {
            this.names = names;
        }

        public static implicit operator bool(BasicConsoleFlag flag)
            => flag.IsUsed;


    }

}
