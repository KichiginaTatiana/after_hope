namespace AfterHope.Commands.Parser
{
    public interface ICommandParser
    {
         Command Parse(string command);
    }
}