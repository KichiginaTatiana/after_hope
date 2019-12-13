namespace AfterHope.Commands.CommandGeneration
{
    public class CommandGenerator : ICommandGenerator
    {
        public string Generate(string commandName, params string[] commandArgs)
        {
            return $"/{commandName}{(commandArgs.Length > 0 ? " " + string.Join(" ", commandArgs) : "")}";
        }
    }
}