namespace AfterHope.Commands
{
    public class Command
    {
        public CommandType Type { get; set; }
        public string[] Args { get; set; }

        public static Command Create(CommandType commandType, params string[] args)
        {
            return new Command
            {
                Type = commandType,
                Args = args,
            };
        }
    }
}