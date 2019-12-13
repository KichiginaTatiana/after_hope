namespace AfterHope.Commands
{
    public interface ICommandManager
    {
         CommandResult Execute(string commandLine, CommandMeta commandMeta);
    }
}