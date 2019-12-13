namespace AfterHope.Commands.CommandGeneration
{
    public interface ICommandGenerator
    {
        string Generate(string commandName, params string[] commandArgs);
    }
}